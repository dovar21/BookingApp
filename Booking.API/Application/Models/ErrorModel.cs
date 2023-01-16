namespace Booking.API.Application.Models;

using Booking.API.Application.Enums;

public class ErrorModel
{
    public ErrorCode Code { get; set; }
    public String Description { get; set; }

    public ErrorModel()
    {

    }

    public ErrorModel(string description)
    {
        Description = description;
    }
    public ErrorModel(string code, string description)
    {
        Code = (ErrorCode)Enum.Parse(typeof(ErrorCode), code);
        Description = description;
    }
    public ErrorModel(ErrorCode code)
    {
        Code = code;
        Description = code.ToString();//X?
    }
}
