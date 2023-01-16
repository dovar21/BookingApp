namespace Booking.API.Application.Models.ServiceObject;

using System;

public class ServiceObjectResponseDTO
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int Amount { get; private set; }
}