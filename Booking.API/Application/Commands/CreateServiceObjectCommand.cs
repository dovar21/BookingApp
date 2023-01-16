namespace Booking.API.Application.Commands;

using MediatR;
using Booking.API.Application.Models;
using Booking.API.Application.Models.ServiceObject;
using System.Runtime.Serialization;

[DataContract]
public record CreateServiceObjectCommand
: IRequest<BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>>
{
    [DataMember]
    public string Name { get; init; }

    [DataMember]
    public int Amount { get; init; }

    public CreateServiceObjectCommand(string name, int amount)
    {
        Name = name;
        Amount = amount;
    }
}