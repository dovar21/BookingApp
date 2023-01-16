using Booking.API.Application.Models;
using Booking.API.Application.Models.ServiceObject;

namespace Booking.API.Application.Queries;

public interface IServiceObjectQueries
{
    Task<BaseDataResponse<ServiceObjectResponseDTO>> FindAsync(Guid id);
    Task<BaseDataResponse<List<ServiceObjectResponseDTO>>> FindAllAsync();
}