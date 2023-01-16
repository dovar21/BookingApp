namespace Booking.API.Application.Models;

using System.Net;

public class BaseResponse : BaseResponse<ErrorModel>
{
    public BaseResponse(HttpStatusCode statusCode, params ErrorModel[] errors) : base(errors)
    {
    }
}

public class BaseResponse<TMessage>
{
    public IList<TMessage> Errors { get; set; }

    public BaseResponse(params TMessage[] errors)
    {
        Errors = errors?.ToList() ?? new List<TMessage>();
    }
    public BaseResponse(IEnumerable<TMessage> errors)
    {
        Errors = errors?.ToList() ?? new List<TMessage>();
    }

}
