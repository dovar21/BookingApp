namespace Booking.API.Application.Commands;

using global::AutoMapper;
using MediatR;
using Booking.API.Application.Enums;
using Booking.API.Application.Models;
using Booking.API.Application.Models.ServiceObject;
using Booking.Domain.AggregatesModel.ServiceObjectAggregate;
using Booking.API.Application.Extensions;

public class UpdateServiceObjectCommandHandler
: IRequestHandler<UpdateServiceObjectCommand, BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>>
{
    private readonly IServiceObjectRepository _serviceObjectRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UpdateServiceObjectCommandHandler(IMediator mediator,
        IMapper mapper,
        IServiceObjectRepository serviceObjectRepository)
    {
        _serviceObjectRepository = serviceObjectRepository ?? throw new ArgumentNullException(nameof(serviceObjectRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>> Handle(UpdateServiceObjectCommand message, CancellationToken cancellationToken)
    {
        //Find service object
        var serviceObject = await _serviceObjectRepository.FindAsync(message.Id);

        if (serviceObject == null)
            return BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>.Fail(null, new ErrorModel(ErrorCode.ServiceObjectNotFound.GetDisplayName()));
        
        //Check service object by name and id
        if (await _serviceObjectRepository.FindAsync(message.Name, message.Id) != null)
            return BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>.Fail(null, new ErrorModel(ErrorCode.ServiceObjectNameMustBeUnique.GetDisplayName()));

        //Check and set amount 
        if (message.Amount != null && message.Amount < 1)
            return BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>.Fail(null, new ErrorModel(ErrorCode.ServiceObjectAmountMustBeGreaterThanZero.GetDisplayName()));

        //Check and set name
        if (!string.IsNullOrEmpty(message.Name) && (message.Name.Length == 0 || message.Name.Length > 100))
            return BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>.Fail(null, new ErrorModel(ErrorCode.ServiceObjectNameLengthMustBeBetween1And100Characters.GetDisplayName()));

        if (message.Amount != null || message.Name != null)
        {
            if(message.Amount != null)
                serviceObject.SetAmount((int)message.Amount);
            
            if(message.Name != null)
                serviceObject.SetName(message.Name);

            //Update
            _serviceObjectRepository.Update(serviceObject);

            //Save changes
            await _serviceObjectRepository.UnitOfWork
                 .SaveEntitiesAsync(cancellationToken);
        }
        return BaseDataResponse<ServiceObjectCreateUpdateResponseDTO>.Success(_mapper.Map<ServiceObjectCreateUpdateResponseDTO>(serviceObject));

    }
}
