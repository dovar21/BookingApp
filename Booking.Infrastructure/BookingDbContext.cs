namespace Booking.Infrastructure;

using Booking.Domain.AggregatesModel.ServiceObjectAggregate;
using Booking.Domain.SeedWork;
using Booking.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

public class BookingDbContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "dbo";
    public DbSet<ServiceObject> ServiceObjects { get; set; }
    public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ServiceObjectEntityTypeConfiguration());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}