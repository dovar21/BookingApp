namespace Booking.Domain.AggregatesModel.ServiceObjectAggregate; 

using Booking.Domain.SeedWork;

public interface IServiceObjectRepository : IRepository<ServiceObject>
{
    ServiceObject Add(ServiceObject serviceObject);
    void Update(ServiceObject serviceObject);
    void Remove(ServiceObject serviceObject);
    Task<ServiceObject> FindAsync(Guid id);
    Task<List<ServiceObject>> FindAllAsync();
    Task<ServiceObject> FindAsync(string name);
    Task<ServiceObject> FindAsync(string name, Guid id);

}

