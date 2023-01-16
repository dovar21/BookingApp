namespace Booking.API.Application.Queries;

using global::AutoMapper;
using Booking.API.Application.Enums;
using Booking.API.Application.Models;
using Booking.API.Application.Models.ServiceObject;
using Booking.Domain.AggregatesModel.ServiceObjectAggregate;
using Booking.API.Application.Extensions;

public class ServiceObjectQueries : IServiceObjectQueries
{
    private readonly IMapper _mapper;
    private readonly IServiceObjectRepository _serviceObjectRepository;

    public ServiceObjectQueries(IMapper mapper, 
        IServiceObjectRepository serviceObjectRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _serviceObjectRepository = serviceObjectRepository ?? throw new ArgumentNullException(nameof(serviceObjectRepository));
    }
    public async Task<BaseDataResponse<ServiceObjectResponseDTO>> FindAsync(Guid id)
    {
        var result = await _serviceObjectRepository.FindAsync(id);

        if (result == null)
            return BaseDataResponse<ServiceObjectResponseDTO>.Fail(null, new ErrorModel(ErrorCode.ServiceObjectNotFound.GetDisplayName()));

        return BaseDataResponse<ServiceObjectResponseDTO>.Success(_mapper.Map<ServiceObjectResponseDTO>(result));
    }
    //View all service objects
    public async Task<BaseDataResponse<List<ServiceObjectResponseDTO>>> FindAllAsync()
    {
        //using (var connection = new SqlConnection(_connectionString)) // using dapper
        //{
        //    connection.Open();

        //    return await connection.QueryAsync<ServiceObjectResponseDTO>("SELECT * FROM ServiceObjects");
        //}

        var result = await _serviceObjectRepository.FindAllAsync();

        return BaseDataResponse<List<ServiceObjectResponseDTO>>.Success(_mapper.Map<List<ServiceObjectResponseDTO>>(result));
    }
}