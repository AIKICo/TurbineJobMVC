namespace TurbineJobMVC.Services
{
    public interface IService
    {
        IWorkOrderService WorkOrderService { get; }
        IDateTimeService DateTimeService { get; }
    }
}