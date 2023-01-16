namespace Booking.API.Application.Enums;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public enum ErrorCode
{
    [Display(Name = "Service object not found")]
    ServiceObjectNotFound = 1,
    [Display(Name = "Service object name must be unique")]
    ServiceObjectNameMustBeUnique = 2,
    [Display(Name = "Service object amount must be greater than zero")]
    ServiceObjectAmountMustBeGreaterThanZero = 3,
    [Display(Name = "Service object name length must be between 1 and 100 characters")]
    ServiceObjectNameLengthMustBeBetween1And100Characters = 4,
    [Display(Name = "Service object amount cannot be null")]
    ServiceObjectAmountCannotBeNull = 5,
    [Display(Name = "Balance exceeded")]
    BalanceExceeded = 6
}