using System.ComponentModel.DataAnnotations.Schema;

namespace TurbineJobMVC.Models.ViewModels
{
    public class NotEndWorkOrderListViewModel
    {
        public string AlarmDate { get; set; }
        public long WONo { get; set; }
        public string Amval { get; set; }

        public string WODate { get; set; }
        public string WOTime { get; set; }
        public string OprCode { get; set; }
        public string OprCodeName { get; set; }
        public bool? SoftWare { get; set; }
        public bool? HardWare { get; set; }
        public bool? NetworkInternet { get; set; }
        public bool? Cons { get; set; }
        public bool? Question { get; set; }
        public string EndJobDate { get; set; }
        public int? TypeOpreration { get; set; }
        public string WorkReport { get; set; }
        public string WorkReportTime { get; set; }
        public string AskerCode { get; set; }
        public string RequestDate { get; set; }
        public string NeedDescription { get; set; }
        public int? ConsComment { get; set; }
        public int? WoType { get; set; }
        public string ManagmentName { get; set; }
        public string OfficeName { get; set; }
        public string RegisterNO { get; set; }
        public int? Priority { get; set; }
        public int? diff { get; set; }

        [Column(TypeName = "numeric(18,6)")] public decimal? diffHour { get; set; }

        public string WoTypeName { get; set; }
        public int ManDays { get; set; }
        public int? CustomerRate { get; set; }
        public double? ManageRate { get; set; }
        public string LastStatus { get; set; }
    }
}