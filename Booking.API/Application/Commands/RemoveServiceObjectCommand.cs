namespace Booking.API.Application.Commands;

using MediatR;
using Booking.API.Application.Models;
using Booking.API.Application.Models.ServiceObject;
using System.Runtime.Serialization;

[DataContract]
public record RemoveServiceObjectCommand
: IRequest<BaseDataResponse<ServiceObjectResponseDTO>>
{
    [DataMember]
    public Guid Id { get; init; }
}