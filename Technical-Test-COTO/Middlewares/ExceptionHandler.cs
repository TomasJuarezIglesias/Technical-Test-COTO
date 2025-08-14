using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Technical_Test_COTO.Middlewares;
public sealed class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is BusinessException businessEx)
        {
            _logger.LogError($"Business Error({businessEx.StatusCode}): {businessEx.Message}");

            context.Response.StatusCode = (int)businessEx.StatusCode;
            await context.Response.WriteAsJsonAsync(ApiResponse<object>.ErrorResponse(businessEx.Message), cancellationToken: cancellationToken);
            return true;
        }

        _logger.LogError(exception, "Unhandled Exception: {Message}", exception.Message);

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsJsonAsync(new
        {
            Message = "Error interno del servidor",
            TraceId = context.TraceIdentifier
        }, cancellationToken: cancellationToken);
        return true;
    }
}