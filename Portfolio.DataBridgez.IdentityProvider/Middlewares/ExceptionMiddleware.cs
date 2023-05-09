using Portfolio.DataBridgez.Domain.Dtos.Get;

namespace Portfolio.DataBridgez.IdentityProvider.Middlewares;

public sealed class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger _logger;

    public ExceptionMiddleware(ILogger logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.Error(exception, "Exception middleware caught exception: {Error}", exception.Message);
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode =  StatusCodes.Status500InternalServerError;

        var message = exception switch
        {
            InvalidOperationException => string.Format("Invalid operation exception caught by exception handler: {0}",
                exception.Message),
            ArgumentNullException => string.Format("Argument null reference exception caught by exception handler: {0}",
                exception.Message),
            ArgumentException => string.Format("Argument exception caught by exception handler: {0}",
                exception.Message),
            _ => "Internal server error caught by exception handler"
        };

        var response = new GlobalExceptionResponse(message,  StatusCodes.Status500InternalServerError);
        await context.Response.WriteAsync(response.ToString());
    }
}