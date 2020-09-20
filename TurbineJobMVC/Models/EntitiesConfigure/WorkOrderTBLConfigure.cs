using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurbineJobMVC.Models.Entities;

namespace TurbineJobMVC.Models.EntitiesConfigure
{
    public class WorkOrderTBLConfigure : IEntityTypeConfiguration<WorkOrderTBL>
    {
        public void Configure(EntityTypeBuilder<WorkOrderTBL> builder)
        {
            builder.HasKey(x => x.WONo);
            builder.Property(x => x.Amval).HasMaxLength(20);
            builder.Property(x => x.WODate).HasMaxLength(10);
            builder.Property(x => x.WOTime).HasMaxLength(5);
            builder.Property(x => x.EndJobDate).HasMaxLength(10);
            builder.Property(x => x.WorkReport).HasMaxLength(4000);
            builder.Property(x => x.WorkReportTime).HasMaxLength(5);
            builder.Property(x => x.RequestDate).HasMaxLength(10);
            builder.Property(x => x.NeedDescription).HasMaxLength(4000);
            builder.Property(x => x.AlarmDate).HasMaxLength(10);
            builder.Property(x => x.AskerName).HasMaxLength(500);
        }
    }
}