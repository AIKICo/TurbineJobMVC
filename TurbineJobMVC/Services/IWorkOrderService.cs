﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TurbineJobMVC.Models.Entites;
using TurbineJobMVC.Models.ViewModels;

namespace TurbineJobMVC.Services
{
    public interface IWorkOrderService
    {
        Task<long> addWorkOrder(JobViewModel JobModel);
        Task<WorkOrderViewModel> GetSingleWorkOrder(string Wono);
        Task<WorkOrderViewModel> GetSingleWorkOrderByAR(string AR);
        Task<TahvilFormsViewModel> GetTahvilForm(string amval);
        Task<IList<TahvilFormsViewModel>> GetTahvilForms(string regNo);
        Task<IList<WorkOrderDailyReportViewModel>> GetWorkOrderReport(string Wono);
        Task<bool> SetWonoVote(long wono);
        bool IsNumberic(string number);
        Task<WorkOrderTBL> IsDublicateActiveAR(string amval);
        Task<WorkOrderTBL> IsDublicateNotRateAR(string amval);
    }
}
