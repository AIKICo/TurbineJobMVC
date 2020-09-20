using System;

namespace TurbineJobMVC.Models.ViewModels
{
    public class WorkOrderDailyReportViewModel

    {
        public Guid ReportID { get; set; }
        public long? Wono { get; set; }
        public string ReportDate { get; set; }
        public string ReportComment { get; set; }
        public string MemberName { get; set; }
    }
}