namespace Booking.API.Application.Commands;

using MediatR;
using Booking.API.Application.Models;
using Booking.API.Application.Models.ServiceObject;
using System.Runtime.Serialization;

[DataContract]
public record BookingCommand
: IRequest<BaseDataResponse<BookingResponseDTO>>
{
    [DataMember]
    public Guid Id { get; init; }

    [DataMember]
    public int Amount { get; init; }

    public BookingCommand(Guid id, int amount)
    {
        Id = id;
        Amount = amount;
    }
}