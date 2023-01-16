namespace Booking.API.Application.Commands;

using global::AutoMapper;
using MediatR;
using Booking.API.Application.Enums;
using Booking.API.Application.Models;
using Booking.API.Application.Models.ServiceObject;
using Booking.Domain.AggregatesModel.ServiceObjectAggregate;
using Booking.API.Application.Extensions;

public class RemoveServiceObjectCommandHandler
: IRequestHandler<RemoveServiceObjectCommand, BaseDataResponse<ServiceObjectResponseDTO>>
{
    private readonly IServiceObjectRepository _serviceObjectRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public RemoveServiceObjectCommandHandler(IMediator mediator,
        IMapper mapper,
        IServiceObjectRepository serviceObjectRepository)
    {
        _serviceObjectRepository = serviceObjectRepository ?? throw new ArgumentNullException(nameof(serviceObjectRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseDataResponse<ServiceObjectResponseDTO>> Handle(RemoveServiceObjectCommand message, CancellationToken cancellationToken)
    {
        //Find service object
        var serviceObject = await _serviceObjectRepository.FindAsync(message.Id);

        if (serviceObject == null)
            return BaseDataResponse<ServiceObjectResponseDTO>.Fail(null, new ErrorModel(ErrorCode.ServiceObjectNotFound.GetDisplayName()));

        //Remove
        _serviceObjectRepository.Remove(serviceObject);

        //Save changes
        await _serviceObjectRepository.UnitOfWork
             .SaveEntitiesAsync(cancellationToken);

        return BaseDataResponse<ServiceObjectResponseDTO>.Success(_mapper.Map<ServiceObjectResponseDTO>(serviceObject));

    }
}