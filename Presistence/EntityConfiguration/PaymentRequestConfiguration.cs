using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.EntityConfiguration
{
    public class PaymentRequestConfiguration : IEntityTypeConfiguration<PaymentRequest>
    {
        public void Configure(EntityTypeBuilder<PaymentRequest> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedNever();
            builder.Property(t => t.CustomerId).IsRequired().HasMaxLength(20);
            builder.Property(t => t.CreatedOn).IsRequired();
            builder.Property(t => t.Amount).IsRequired();
            builder.Property(t => t.Reference).IsRequired().HasMaxLength(50);
            builder.Property(t => t.StatusReasonId).IsRequired();            
        }
    }
}