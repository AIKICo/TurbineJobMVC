using Audit.Core;
using Audit.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TurbineJobMVC.Models.Entities;
using TurbineJobMVC.Models.EntitiesConfigure;

namespace TurbineJobMVC.Models
{
    public class PCStockDBContext : AuditDbContext
    {
        private readonly ILogger<PCStockDBContext> _logger;
        private readonly IHttpContextAccessor _accessor;
        public DbSet<WorkOrderTBL> WorkOrderTBL { get; set; }
        public DbSet<WorkOrder> WorkOrder { get; set; }
        public DbSet<TahvilForms> TahvilForms { get; set; }
        public DbSet<WorkOrderDailyReportTBL> WorkOrderDailyReportTBL { get; set; }
        public DbSet<NotEndWorkOrderList> NotEndWorkOrderList { get; set; }

        public PCStockDBContext(ILogger<PCStockDBContext> logger, IHttpContextAccessor accessor) : base()
        {
            Audit.Core.Configuration.Setup().UseNullProvider();
            _logger = logger;
            _accessor = accessor;
        }

        public PCStockDBContext(DbContextOptions options, ILogger<PCStockDBContext> logger, IHttpContextAccessor accessor) : base(options)
        {
            Audit.Core.Configuration.Setup().UseNullProvider();
            _logger = logger;
            _accessor = accessor;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging(false);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<WorkOrderTBL>(new WorkOrderTBLConfigure());
            modelBuilder.ApplyConfiguration<WorkOrder>(new BaseViewConfigure<WorkOrder>());
            modelBuilder.ApplyConfiguration<NotEndWorkOrderList>(new BaseViewConfigure<NotEndWorkOrderList>());
            modelBuilder.ApplyConfiguration<TahvilForms>(new TahvilFormsConfigure());
            modelBuilder.ApplyConfiguration<WorkOrderDailyReportTBL>(new WorkOrderDailyReportTBLConfigure());
        }

        public override void OnScopeSaving(IAuditScope auditScope)
        {
            _logger.LogInformation("Audit event recorded: {event}", new
            {
                IPAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                Event = auditScope.Event
            });
        }
    }
}
