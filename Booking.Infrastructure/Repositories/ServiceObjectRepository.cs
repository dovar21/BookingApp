namespace Booking.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Booking.Domain.AggregatesModel.ServiceObjectAggregate;
using Booking.Domain.SeedWork;

public class ServiceObjectRepository: IServiceObjectRepository
{
    private readonly BookingDbContext _context;

    public IUnitOfWork UnitOfWork
    {
        get
        {
            return _context;
        }
    }

    public ServiceObjectRepository(BookingDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public ServiceObject Add(ServiceObject serviceObject)
    {
        return _context.ServiceObjects.Add(serviceObject).Entity;
    }

    public async Task<ServiceObject> FindAsync(Guid id)
    {
        return await _context
                            .ServiceObjects
                            .FirstOrDefaultAsync(o => o.Id == id);
    }
    public async Task<List<ServiceObject>> FindAllAsync()
    {
        return await _context
                            .ServiceObjects.AsNoTracking().ToListAsync();
    }
    public async Task<ServiceObject> FindAsync(string name)
    {
        return await _context
                            .ServiceObjects
                            .FirstOrDefaultAsync(o => o.Name == name);
    }
    public async Task<ServiceObject> FindAsync(string name, Guid id)
    {
        return await _context
                            .ServiceObjects
                            .FirstOrDefaultAsync(o => o.Name == name && o.Id != id);
    }
    public void Update(ServiceObject serviceObject)
    {
        _context.Entry(serviceObject).State = EntityState.Modified;
    }
    public void Remove(ServiceObject serviceObject)
    {
        _context.ServiceObjects.Remove(serviceObject);
    }
}
