namespace TurbineJobMVC.Models.Entities
{
    public class WorkOrderTBL
    {
        public long WONo { get; set; }
        public string Amval { get; set; }
        public string WODate { get; set; }
        public string WOTime { get; set; }
        public int? OprCode { get; set; }
        public bool SoftWare { get; set; }
        public bool HardWare { get; set; }
        public bool NetworkInternet { get; set; }
        public bool Question { get; set; }
        public bool Cons { get; set; }
        public string EndJobDate { get; set; }
        public int TypeOpreration { get; set; }
        public string WorkReport { get; set; }
        public string WorkReportTime { get; set; }
        public int AskerCode { get; set; }
        public string RequestDate { get; set; }
        public string NeedDescription { get; set; }
        public int ConsComment { get; set; }
        public int WoType { get; set; }
        public string AlarmDate { get; set; }
        public string AskerName { get; set; }
        public int ManDays { get; set; }
        public int? CustomerRate { get; set; }
    }
}