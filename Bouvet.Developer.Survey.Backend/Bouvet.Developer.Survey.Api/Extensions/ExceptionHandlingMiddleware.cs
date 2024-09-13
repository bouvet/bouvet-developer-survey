using System.ComponentModel.DataAnnotations;
using Bouvet.Developer.Survey.Domain.Exceptions;

namespace Bouvet.Developer.Survey.Api.Extensions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred while processing the request.");

            var response = context.Response;
            response.ContentType = "application/json"; // Explicitly set Content-Type to JSON

            var statusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ArgumentException => StatusCodes.Status400BadRequest,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };

            response.StatusCode = statusCode;

            var errorDetails = new
            {
                StatusCode = statusCode, ex.Message,
            };

            // Write JSON response to the body
            await response.WriteAsJsonAsync(errorDetails);
        }
    }
}
