using Ambev.DeveloperEvaluation.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Logging;
using PhoneNumbers;

namespace Ambev.DeveloperEvaluation.Infrastructure.Services.Validators;

public class PhoneService : IPhoneService
{
    private readonly ILogger<PhoneService> _logger;

    public PhoneService(ILogger<PhoneService> logger)
    {
        _logger = logger;
    }

    public bool IsValid(string idd, string number)
    {
        try
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtil.Parse($"+{idd}{number}", null);
            return phoneNumberUtil.IsValidNumber(phoneNumber);
        }
        catch (NumberParseException ex)
        {
            _logger.LogError(ex, "An error occurred while validating phone number: {idd}{number}", idd, number);
            return false;
        }
    }
}