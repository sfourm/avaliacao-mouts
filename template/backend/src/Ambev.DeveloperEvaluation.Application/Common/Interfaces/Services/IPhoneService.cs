namespace Ambev.DeveloperEvaluation.Application.Common.Interfaces.Services;

public interface IPhoneService
{
    bool IsValid(string idd, string number);
}