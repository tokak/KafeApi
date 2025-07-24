using Microsoft.AspNetCore.Http;
using Serilog;
using System.Diagnostics;

namespace KafeApi.Persistence.Middlewares;

public class SerilogMiddleware
{
    private readonly RequestDelegate _next;

    public SerilogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        var request = context.Request;
        var ip = context.Connection.RemoteIpAddress?.ToString();

    }
}
