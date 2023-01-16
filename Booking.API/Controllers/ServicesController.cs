namespace Booking.API.Controllers;

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Booking.API.Application.Commands;
using Booking.API.Application.Models;
using Booking.API.Application.Models.ServiceObject;
using Booking.API.Application.Queries;

[Route("api/services")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IServiceObjectQueries _serviceObjectQueries;
    public static List<BookingResponseDTO> bookingList = new();
    public ServicesController(IMapper mapper,
        IMediator mediator,
        IServiceObjectQueries serviceObjectQueries)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _serviceObjectQueries = serviceObjectQueries ?? throw new ArgumentNullException(nameof(serviceObjectQueries));
    }

    [HttpGet("{id}")]
    public async Task<BaseDataResponse<ServiceObjectResponseDTO>> GetServiceObjectByIdAsync(Guid id)
    {
        return await _serviceObjectQueries.FindAsync(id);
    }
    [HttpGet]
    public async Task<BaseDataResponse<List<ServiceObjectResponseDTO>>> GetServiceObjects()
    {
        return await _serviceObjectQueries.FindAllAsync();
    }
    [Route("booking")]
    [HttpPost]
    public async Task<BaseDataResponse<BookingResponseDTO>> Booking([FromBody] BookingCommand command)
    {
        var result = await _mediator.Send(command);

        //Add success booking to list - хранение сущностей в бд в памяти, данных о бронях в коллекции в памяти(ИЗ ТЗ)
        //Наскольо я понял, все успешные бронирования будуд храняться в коллекции.
        if (result.Data != null && result.Data.Ok)
            bookingList.Add(result.Data);

        return result;
    }
    [Route("create")]
    [HttpPost]
    public async Task<BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>> CreateAsync([FromBody] CreateServiceObjectCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut("update/{id}")]
    public async Task<BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateServiceObjectCommand command)
    {
        command.Id = id;

        return await _mediator.Send(command);
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<BaseDataResponse<ServiceObjectResponseDTO>> RemoveAsync(Guid id)
    {
        return await _mediator.Send(new RemoveServiceObjectCommand { Id = id });
    }
}