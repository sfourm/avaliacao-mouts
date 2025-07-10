using FluentValidation;
using MediatR;
using ValidationException = Ambev.DeveloperEvaluation.Packages.Application.Exceptions.ValidationException;

namespace Ambev.DeveloperEvaluation.Packages.Application.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        ValidationContext<TRequest> context = new(request);
        var validationResults =
            await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures =
            validationResults.Where(r => r.Errors.Count > 0).SelectMany(r => r.Errors).ToList();

        if (failures.Count > 0) throw new ValidationException(failures);

        return await next();
    }
}