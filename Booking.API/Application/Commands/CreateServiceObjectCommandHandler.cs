namespace Booking.API.Application.Commands;

using global::AutoMapper;
using MediatR;
using Booking.API.Application.Enums;
using Booking.API.Application.Models;
using Booking.API.Application.Models.ServiceObject;
using Booking.Domain.AggregatesModel.ServiceObjectAggregate;
using Booking.API.Application.Extensions;

public class CreateServiceObjectCommandHandler
: IRequestHandler<CreateServiceObjectCommand, BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>>
{
    private readonly IServiceObjectRepository _serviceObjectRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateServiceObjectCommandHandler(IMediator mediator,
        IMapper mapper,
        IServiceObjectRepository serviceObjectRepository)
    {
        _serviceObjectRepository = serviceObjectRepository ?? throw new ArgumentNullException(nameof(serviceObjectRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>> Handle(CreateServiceObjectCommand message, CancellationToken cancellationToken)
    {
        //Check amount
        if (message.Amount == null || message.Amount < 1)
            return BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>.Fail(null, new ErrorModel(ErrorCode.ServiceObjectAmountMustBeGreaterThanZero.GetDisplayName()));

        //Check name
        if (message.Name == null || message.Name.Length == 0 || message.Name.Length > 100)
            return BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>.Fail(null, new ErrorModel(ErrorCode.ServiceObjectNameLengthMustBeBetween1And100Characters.GetDisplayName()));

        //Find service object by name
        if (await _serviceObjectRepository.FindAsync(message.Name) != null)
            return BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>.Fail(null, new ErrorModel(ErrorCode.ServiceObjectNameMustBeUnique.GetDisplayName()));

        //Create new service object
        ServiceObject serviceObject = new (message.Name, message.Amount);

        //Add
        var result = _serviceObjectRepository.Add(serviceObject);

        //Save changes
        await _serviceObjectRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);

        return BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>.Success(_mapper.Map<ServiceObjectCreateUpdateResponseDTO>(result));

    }
}
