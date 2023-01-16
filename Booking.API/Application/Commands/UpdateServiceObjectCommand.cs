namespace Booking.API.Application.Commands;

using MediatR;
using Booking.API.Application.Models;
using Booking.API.Application.Models.ServiceObject;
using System.Runtime.Serialization;

[DataContract]
public record UpdateServiceObjectCommand : IRequest<BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>>
{
    [DataMember]
    public Guid Id { get; set; }
    [DataMember]
    public string? Name { get; init; }

    [DataMember]
    public int? Amount { get; init; }
    public UpdateServiceObjectCommand(string? name, int? amount)
    {
        Name = name;
        Amount = amount;
    }
}
