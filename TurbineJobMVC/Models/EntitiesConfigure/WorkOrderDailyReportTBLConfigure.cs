using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurbineJobMVC.Models.Entities;

namespace TurbineJobMVC.Models.EntitiesConfigure
{
    public class WorkOrderDailyReportTBLConfigure : IEntityTypeConfiguration<WorkOrderDailyReportTBL>
    {
        public void Configure(EntityTypeBuilder<WorkOrderDailyReportTBL> builder)
        {
            builder.HasKey(x => x.ReportID);
            builder.Property(x => x.ReportComment).HasMaxLength(4000);
            builder.HasQueryFilter(x => !string.IsNullOrEmpty(x.ReportComment));
        }
    }
}