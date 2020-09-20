using Audit.Core;
using Audit.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TurbineJobMVC.Models.Entities;
using TurbineJobMVC.Models.EntitiesConfigure;
using Configuration = Audit.Core.Configuration;

namespace TurbineJobMVC.Models
{
    public class PCStockDBContext : AuditDbContext
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<PCStockDBContext> _logger;

        public PCStockDBContext(ILogger<PCStockDBContext> logger, IHttpContextAccessor accessor)
        {
            Configuration.Setup().UseNullProvider();
            _logger = logger;
            _accessor = accessor;
        }

        public PCStockDBContext(DbContextOptions options, ILogger<PCStockDBContext> logger,
            IHttpContextAccessor accessor) : base(options)
        {
            Configuration.Setup().UseNullProvider();
            _logger = logger;
            _accessor = accessor;
        }

        public DbSet<WorkOrderTBL> WorkOrderTBL { get; set; }
        public DbSet<WorkOrder> WorkOrder { get; set; }
        public DbSet<TahvilForms> TahvilForms { get; set; }
        public DbSet<WorkOrderDailyReportTBL> WorkOrderDailyReportTBL { get; set; }
        public DbSet<NotEndWorkOrderList> NotEndWorkOrderList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging(false);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new WorkOrderTBLConfigure());
            modelBuilder.ApplyConfiguration(new BaseViewConfigure<WorkOrder>());
            modelBuilder.ApplyConfiguration(new BaseViewConfigure<NotEndWorkOrderList>());
            modelBuilder.ApplyConfiguration(new TahvilFormsConfigure());
            modelBuilder.ApplyConfiguration(new WorkOrderDailyReportTBLConfigure());
        }

        public override void OnScopeSaving(IAuditScope auditScope)
        {
            _logger.LogInformation("Audit event recorded: {event}", new
            {
                IPAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(), auditScope.Event
            });
        }
    }
}