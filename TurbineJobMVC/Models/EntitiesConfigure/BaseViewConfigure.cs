using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TurbineJobMVC.Models.EntitiesConfigure
{
    public class BaseViewConfigure<T> : IEntityTypeConfiguration<T> where T : class
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasNoKey();
        }
    }
}