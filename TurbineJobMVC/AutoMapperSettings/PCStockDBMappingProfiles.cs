using AutoMapper;
using MD.PersianDateTime.Standard;
using TurbineJobMVC.Models.Entities;
using TurbineJobMVC.Models.ViewModels;

namespace TurbineJobMVC.AutoMapperSettings
{
    public class PCStockDBMappingProfiles : Profile
    {
        public PCStockDBMappingProfiles()
        {
            CreateMap<WorkOrderTBL, JobViewModel>()
                .ForMember(x => x.AR, opt => opt.MapFrom(x => x.Amval)).ReverseMap();
            CreateMap<WorkOrder, WorkOrderViewModel>().ReverseMap();
            CreateMap<TahvilForms, TahvilFormsViewModel>().ReverseMap();
            CreateMap<WorkOrderDailyReportTBL, WorkOrderDailyReportViewModel>()
                .ForMember(q => q.ReportDate,
                    opt => opt.MapFrom(q => new PersianDateTime(q.ReportDate).ToLongDateTimeString()))
                .ReverseMap();
            CreateMap<NotEndWorkOrderList, NotEndWorkOrderListViewModel>().ReverseMap();
        }
    }
}