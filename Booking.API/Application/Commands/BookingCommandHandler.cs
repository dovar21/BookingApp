namespace Booking.API.Application.Commands;

using global::AutoMapper;
using MediatR;
using Booking.API.Application.Enums;
using Booking.API.Application.Models;
using Booking.API.Application.Models.ServiceObject;
using Booking.Domain.AggregatesModel.ServiceObjectAggregate;
using Booking.API.Application.Extensions;

public class BookingCommandCommandHandler
: IRequestHandler<BookingCommand, BaseDataResponse<BookingResponseDTO>>
{
    private readonly IServiceObjectRepository _serviceObjectRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public BookingCommandCommandHandler(IMediator mediator,
        IMapper mapper,
        IServiceObjectRepository serviceObjectRepository)
    {
        _serviceObjectRepository = serviceObjectRepository ?? throw new ArgumentNullException(nameof(serviceObjectRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseDataResponse<BookingResponseDTO>> Handle(BookingCommand message, CancellationToken cancellationToken)
    {
        bool ok = false;

        //Find service object
        var serviceObject = await _serviceObjectRepository.FindAsync(message.Id);

        if (serviceObject == null)
            return BaseDataResponse<BookingResponseDTO>.Fail(null, new ErrorModel(ErrorCode.ServiceObjectNotFound.GetDisplayName()));

        //Check amount
        if (message.Amount == null)
            return BaseDataResponse<BookingResponseDTO>.Fail(null, new ErrorModel(ErrorCode.ServiceObjectAmountCannotBeNull.GetDisplayName()));

        //Если <0, то оборудование как возвращается (текущий остаток увеличивается) - ИЗ ТЗ
        //Increase amount
        if (message.Amount < 0)
            serviceObject.IncreaseAmount(1);

        //Если = 0, то ничего не делаем (просто возвращаем текущий остаток) - ИЗ ТЗ
        if (message.Amount == 0)
            return BaseDataResponse<BookingResponseDTO>.Success(_mapper.Map<BookingResponseDTO>(serviceObject));

        if (message.Amount > 0)
        {
            var x = ErrorCode.BalanceExceeded.GetDisplayName();
            if (serviceObject.Amount >= message.Amount)
                serviceObject.DecreaseAmount(message.Amount);
            else
                return BaseDataResponse<BookingResponseDTO>.Fail(null, new ErrorModel(ErrorCode.BalanceExceeded.GetDisplayName()));

            //Success
            ok = true;
        }
        if (message.Amount != 0)
        {
            //Update
            _serviceObjectRepository.Update(serviceObject);

            //Save changes
            await _serviceObjectRepository.UnitOfWork
                 .SaveEntitiesAsync(cancellationToken);
        }

        var result = _mapper.Map<BookingResponseDTO>(serviceObject);

        if (ok)
            result.Ok = ok;

        return BaseDataResponse<BookingResponseDTO>.Success(result);

       
    }
}
