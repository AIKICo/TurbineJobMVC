namespace TurbineJobMVC.Services
{
    public class Service : IService
    {
        public Service(IWorkOrderService workOrderService, IDateTimeService dateTimeService)
        {
            WorkOrderService = workOrderService;
            DateTimeService = dateTimeService;
        }

        public IWorkOrderService WorkOrderService { get; }

        public IDateTimeService DateTimeService { get; }
    }
}