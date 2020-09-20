using System.Collections.Generic;
using System.Threading.Tasks;
using TurbineJobMVC.Models.Entities;
using TurbineJobMVC.Models.ViewModels;

namespace TurbineJobMVC.Services
{
    public interface IWorkOrderService
    {
        Task<long> addWorkOrder(JobViewModel JobModel);
        Task<WorkOrderViewModel> GetSingleWorkOrder(string Wono);
        Task<WorkOrderViewModel> GetSingleWorkOrderByAR(string AR);
        Task<WorkOrderViewModel> ChooseSingleWorkOrderByAROrWono(string code);
        Task<TahvilFormsViewModel> GetTahvilForm(string amval);
        Task<IList<TahvilFormsViewModel>> GetTahvilForms(string regNo);
        Task<IList<WorkOrderDailyReportViewModel>> GetWorkOrderReport(string Wono);
        Task<bool> SetWonoVote(long wono);
        bool IsNumberic(string number);
        WorkOrderTBL IsDublicateActiveAR(string amval);
        Task<WorkOrderTBL> IsDublicateActiveARAsync(string amval);
        WorkOrderTBL IsDublicateNotRateAR(string amval);
        Task<WorkOrderTBL> IsDublicateNotRateARAsync(string amval);
        Task<List<NotEndWorkOrderListViewModel>> GetNotEndWorkOrderList();
        Task<IList<WorkOrderViewModel>> WorkOrderArchive(string AR);
    }
}