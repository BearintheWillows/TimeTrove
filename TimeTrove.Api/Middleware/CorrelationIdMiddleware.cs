using Serilog.Context;

namespace TimeTrove.Api.Middleware;

/// <summary>
/// Middleware for managing and propagating a Correlation ID through HTTP requests.
/// </summary>
/// <remarks>
/// The CorrelationIdMiddleware extracts or generates a unique identifier for each HTTP request.
/// The identifier is primarily sourced from the "X-Correlation-ID" header if provided;
/// otherwise, a new GUID is generated. The Correlation ID is added to the response headers
/// and is also pushed into the logging context to allow traceability of logs per request.
/// </remarks>
/// <example>
/// Note: Direct implementation details or examples are omitted as per the documentation policy.
/// </example>
public class CorrelationIdMiddleware(RequestDelegate next)
{
    private const string HeaderKey = "X-Correlation-ID";

    public async Task Invoke(HttpContext context)
    {
        var correlationId = context.Request.Headers[HeaderKey].FirstOrDefault() ?? Guid.NewGuid().ToString();

        LogContext.PushProperty("CorrelationId", correlationId);
        context.Response.Headers[HeaderKey] = correlationId;

        await next(context);
    }
}