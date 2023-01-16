namespace Booking.API.Application.Models;

public class BaseDataResponse<TData> : BaseDataResponse<TData, ErrorModel>
{
    public BaseDataResponse(TData data, params ErrorModel[] errors) : base(data, errors)
    {

    }
    public static BaseDataResponse<TData> Success(TData model) => new BaseDataResponse<TData>(model);

    public static BaseDataResponse<TData> Fail(TData model, params ErrorModel[] errors) => new BaseDataResponse<TData>(model, errors);
}

public class BaseDataResponse<TData, TMessage> : BaseResponse<TMessage>
{
    public TData Data { get; set; }

    public BaseDataResponse(TData data, params TMessage[] errors) : base(errors)
    {
        //StatusCode = HttpStatusCode.BadRequest;

        Data = data;
    }
    public BaseDataResponse(TData data, IEnumerable<TMessage> errors) : base(errors)
    {
        //StatusCode = HttpStatusCode.BadRequest;

        Data = data;
    }
}
