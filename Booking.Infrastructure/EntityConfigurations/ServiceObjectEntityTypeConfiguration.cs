namespace Booking.Infrastructure.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Booking.Domain.AggregatesModel.ServiceObjectAggregate;

class ServiceObjectEntityTypeConfiguration : IEntityTypeConfiguration<ServiceObject>
{
    public void Configure(EntityTypeBuilder<ServiceObject> config)
    {
        config.ToTable("ServiceObjects", BookingDbContext.DEFAULT_SCHEMA);

        config.HasKey(o => o.Id);

        config.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        config.Property(e => e.Amount)
            .IsRequired();
    }
}