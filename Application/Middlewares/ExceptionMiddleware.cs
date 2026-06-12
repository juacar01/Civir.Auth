using Civir.Auth.Application.Errors;
using Civir.Auth.Application.Exceptions;
using Newtonsoft.Json;
using System.Linq;
using System.Net;

namespace Civir.Auth.Application.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Unhandled exception occurred while processing request {Method} {Path}", context.Request.Method, context.Request.Path);

        var statusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,
            ConflictException => StatusCodes.Status409Conflict,
            FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var result = exception switch
        {
            FluentValidation.ValidationException validationException => CreateValidationResult(statusCode, validationException),
            _ => CreateErrorResult(statusCode, exception)
        };

        await context.Response.WriteAsync(result);
    }

    private static string CreateValidationResult(int statusCode, FluentValidation.ValidationException validationException)
    {
        var errors = validationException.Errors.Select(e => e.ErrorMessage).ToArray();
        var validationJson = JsonConvert.SerializeObject(errors);

        return JsonConvert.SerializeObject(new CodeErrorException(statusCode, errors, validationJson));
    }

    private static string CreateErrorResult(int statusCode, Exception exception)
    {
        var errors = statusCode == StatusCodes.Status500InternalServerError
            ? new[] { "Ocurrió un error interno en el servidor." }
            : new[] { exception.Message };

        return JsonConvert.SerializeObject(new CodeErrorException(statusCode, errors, null));
    }
}
