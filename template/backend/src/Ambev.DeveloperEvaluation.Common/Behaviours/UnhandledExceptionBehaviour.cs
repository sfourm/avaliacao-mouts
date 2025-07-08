using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using Ambev.DeveloperEvaluation.Common.Exceptions;
using Ambev.DeveloperEvaluation.Common.Extensions;
using OpenTelemetry.Trace;

namespace Ambev.DeveloperEvaluation.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        using (_logger.BeginScope(GenerateLogScopeItems()))
        {
            try
            {
                return await next();
            }
            catch (ValidationException validationException)
            {
                LogValidationException(validationException, requestName, request);
                throw;
            }
            catch (ArgumentException argumentException)
            {
                LogException(argumentException, requestName, request);
                throw;
            }
            catch (Exception ex) when (ex is TaskCanceledException { CancellationToken.IsCancellationRequested: true }
                                           or OperationCanceledException)
            {
                LogException(ex, requestName, request);
                throw;
            }
            catch (Exception ex) when (ex.InnerException is TaskCanceledException or OperationCanceledException)
            {
                LogExceptionWithInnerException(ex, requestName, request);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled {Exception} was thrown by Request {Name} {Request}", ex.GetName(),
                    requestName, request);
                Activity.Current?.RecordException(ex);
                Activity.Current?.SetStatus(ActivityStatusCode.Error);
                throw;
            }
        }
    }
    
    private void LogValidationException(ValidationException validationException, string requestName, TRequest request)
    {
        var errors = validationException.Errors?.Select(error => new { error.Key, error.Value }).ToJson() ??
                     string.Empty;
        _logger.LogWarning(validationException,
            "A {Exception} was thrown by the Request {Name} {Request}. Errors: {Errors}", validationException.GetName(),
            requestName, request, errors);
    }

    private void LogException(Exception ex, string requestName, TRequest request)
    {
        _logger.LogWarning(ex, "A {Exception} was thrown by the Request {Name} {Request}", ex.GetName(), requestName,
            request);
    }
    
    private List<KeyValuePair<string, object>> GenerateLogScopeItems()
    {
        return Baggage.Current.GetBaggage()
            .Select(x => new KeyValuePair<string, object>(x.Key, x.Value))
            .ToList();
    }
    
    private void LogExceptionWithInnerException(Exception ex, string requestName, TRequest request)
    {
        _logger.LogWarning(ex,
            "A {Exception} with InnerException {InnerException} was thrown by the Request {Name} {Request}",
            ex.GetName(), ex.InnerException!.GetType().Name, requestName, request);
    }
}