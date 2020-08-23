using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.EntityConfiguration
{
    public class StatusReasonConfiguration : IEntityTypeConfiguration<StatusReason>
    {
        public void Configure(EntityTypeBuilder<StatusReason> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedNever();
            builder.Property(t => t.Description).IsRequired().HasMaxLength(50);

        }
    }
}
