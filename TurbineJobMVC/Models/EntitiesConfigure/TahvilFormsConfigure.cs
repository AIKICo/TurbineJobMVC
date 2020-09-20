using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurbineJobMVC.Models.Entities;

namespace TurbineJobMVC.Models.EntitiesConfigure
{
    public class TahvilFormsConfigure : BaseViewConfigure<TahvilForms>
    {
        public override void Configure(EntityTypeBuilder<TahvilForms> builder)
        {
            base.Configure(builder);
            builder.HasQueryFilter(q => (q.TypeTahvil != 4) & (q.AmvalNo != 0));
        }
    }
}