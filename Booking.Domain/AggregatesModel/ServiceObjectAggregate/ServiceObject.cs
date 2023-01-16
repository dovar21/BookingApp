
namespace Booking.Domain.AggregatesModel.ServiceObjectAggregate;

using Booking.Domain.SeedWork;

public class ServiceObject: Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public int Amount { get; private set; }

    public ServiceObject(string name, int amount)
    {
        Name = name;
        Amount = amount;
    }
    public void SetName(string name)
    {
        Name = name;
    }
    public void SetAmount(int amount)
    {
        Amount = amount;
    }
    public void IncreaseAmount(int amount)
    {
        Amount = Amount + amount ;
    }
    public void DecreaseAmount(int amount)
    {
        Amount = Amount - amount;
    }
}
