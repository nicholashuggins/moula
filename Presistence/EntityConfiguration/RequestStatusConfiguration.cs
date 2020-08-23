using Microsoft.EntityFrameworkCore;
using Persistence.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.EntityConfiguration
{
    public class RequestStatusConfiguration : IEntityTypeConfiguration<RequestStatus>
    {
        public void Configure(EntityTypeBuilder<RequestStatus> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedNever();
            builder.Property(t => t.Description).IsRequired().HasMaxLength(20);
        }
    }
}
