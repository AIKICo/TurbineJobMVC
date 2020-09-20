using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using DNTCaptcha.Core;
using DNTCaptcha.Core.Providers;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TurbineJobMVC.Models.ViewModels;
using TurbineJobMVC.Services;

namespace TurbineJobMVC.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ILogger<HomeController> logger,
            IMapper map,
            IService service,
            IDataProtectionProvider provider) : base(logger, map, service, provider)
        {
        }

        [HttpGet]
        public IActionResult Index(bool isDefaulAR)
        {
            return View();
        }

        [HttpGet]
        public IActionResult IndexByAR(string id)
        {
            return View("Index", new JobViewModel {AR = id, defaultAR = true});
        }

        [HttpGet]
        public IActionResult IndexByARConflict(string id)
        {
            return View("Index",
                new JobViewModel
                    {AR = id, Description = "شماره اموال مغایرت دارد.", defaultAR = true, defaultDes = true});
        }

        [HttpGet]
        public async Task<IActionResult> ArchiveWorkOrder(string id)
        {
            return View(await _service.WorkOrderService.WorkOrderArchive(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(
            ErrorMessage = "لطفا کد امنیتی را وارد نمایید",
            CaptchaGeneratorLanguage = Language.Persian,
            CaptchaGeneratorDisplayMode = DisplayMode.ShowDigits)]
        public async Task<IActionResult> Index(JobViewModel JobModel)
        {
            if (ModelState.IsValid)
            {
                var Wono = await _service.WorkOrderService.addWorkOrder(JobModel);
                if (Wono == -1) throw new Exception();
                return RedirectToAction("Tracking", new {id = Wono.ToString()});
            }

            return View(JobModel);
        }

        public async Task<IActionResult> Tracking(string id)
        {
            var workOrder = await _service.WorkOrderService.GetSingleWorkOrder(id);
            if (workOrder != null)
            {
                ViewData["TahvilInfo"] = await _service.WorkOrderService.GetTahvilForm(workOrder.Amval);
                return View(workOrder);
            }

            return BadRequest(ModelState);
        }

        [ResponseCache(Duration = 100, VaryByQueryKeys = new[] {"*"})]
        public async Task<IActionResult> Search(string id)
        {
            return View(await _service.WorkOrderService.GetTahvilForms(id));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult SelectAction(string txtSearch)

        {
            if (txtSearch.Length < 7)
                return RedirectToAction("Search", new {id = txtSearch});

            return RedirectToAction("WorkOrderReport", new {WonoSearch = txtSearch});
        }


        public async Task<IActionResult> WorkOrderReport(string WonoSearch)
        {
            WorkOrderViewModel workOrder = null;
            ViewData["searchKey"] = WonoSearch;
            if (!_service.WorkOrderService.IsNumberic(WonoSearch)) return BadRequest(ModelState);
            workOrder = await _service.WorkOrderService.ChooseSingleWorkOrderByAROrWono(WonoSearch);
            if (workOrder != null)
            {
                ViewData["WorkOrderInfo"] = workOrder;

                return View(await _service.WorkOrderService.GetWorkOrderReport(workOrder.WONo.ToString()));
            }

            return BadRequest(ModelState);
        }

        public async Task<IActionResult> GetVote(long Vote_Wono)
        {
            if (await _service.WorkOrderService.SetWonoVote(Vote_Wono))
                return View();
            return BadRequest(ModelState);
        }
    }
}