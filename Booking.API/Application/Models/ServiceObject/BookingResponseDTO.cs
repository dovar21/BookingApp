namespace Booking.API.Application.Models.ServiceObject;

using System;

public class BookingResponseDTO
{
    public bool Ok { get; set; } = false;
    public int Amount { get; private set; }
}