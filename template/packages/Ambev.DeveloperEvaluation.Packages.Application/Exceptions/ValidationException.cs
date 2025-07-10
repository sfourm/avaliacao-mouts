using System.Text.Json;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Packages.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException() : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => JsonNamingPolicy.CamelCase.ConvertName(failureGroup.Key),
                failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}