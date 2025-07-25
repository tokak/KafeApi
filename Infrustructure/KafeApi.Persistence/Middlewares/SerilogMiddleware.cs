using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;
using System.Diagnostics;

namespace KafeApi.Persistence.Middlewares;

public class SerilogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SerilogMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
    {
        _next = next;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Invoke(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        var request = context.Request;
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "IP bilinmiyor";

        var claim = _httpContextAccessor.HttpContext?.User?.FindFirst("_e");
        var username = claim != null ? claim.Value : "Anonim";

        using (LogContext.PushProperty("Username", username))
        using (LogContext.PushProperty("RequestPath", request.Path))
        using (LogContext.PushProperty("RequestMethod", request.Method))
        using (LogContext.PushProperty("RequestIP", ip))
        {
            Log.Information("Yeni istek: {@Method} {@Path} - IP: {@IP} - Kullanıcı: {@Username}",
                request.Method, request.Path, ip, username);

            try
            {
                await _next(context);
                sw.Stop();
                Log.Information("Yanıt: {@StatusCode} - Süre: {@Elapsed} ms",
                    context.Response.StatusCode, sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                sw.Stop();
                Log.Error(ex, "Hata oluştu. IP: {@IP} - Süre: {@Elapsed} ms", ip, sw.ElapsedMilliseconds);
                throw;
            }
        }
    }
}
