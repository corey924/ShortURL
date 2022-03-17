using Microsoft.AspNetCore.Http;
using ShortURL.Services.Commons;
using ShortURL.Services.Database.Entity;
using ShortURL.Services.Database.Models;
using ShortURL.Services.Interfaces;

namespace ShortURL.Services.Services
{
  public class LogService : EntityService, ILog
  {
    private readonly ShortUrlDbContext _db;

    public LogService(ShortUrlDbContext db) : base(db)
    {
      this._db = db;
    }

    public async Task<ApiResponse> UrlRedirectionWriteLogAsync(HttpContext httpContext, UrlRedirection urlRedirectionData)
    {
      string deviceInfo = httpContext.Request.Headers["User-Agent"];
      if (!string.IsNullOrEmpty(deviceInfo) && (deviceInfo.Length > 255))
      {
        deviceInfo = deviceInfo.Substring(0, 255);
      }
      var actionLog = new UrlRedirectionLogs()
      {
        Code = urlRedirectionData.Code,
        ToUrl = urlRedirectionData.Url,
        IpAddress = httpContext.Connection?.RemoteIpAddress?.ToString() ?? "",
        DeviceInfo = deviceInfo ?? ""
      };

      _db.ChangeTracker.AutoDetectChangesEnabled = false;
      await _db.UrlRedirectionLogs!.AddAsync(actionLog);
      await _db.SaveChangesAsync();

      return _apiResult.HttpOk();
    }

    public async Task<ApiResponse> ActionWriteLogAsync(HttpContext httpContext, string bodyContent)
    {
      string deviceInfo = httpContext.Request.Headers["User-Agent"];
      if (!string.IsNullOrEmpty(deviceInfo) && (deviceInfo.Length > 255))
      {
        deviceInfo = deviceInfo.Substring(0, 255);
      }

      var token = httpContext.Request.Headers["token"].ToString();

      var actionLog = new ActionLogs()
      {
        Device = deviceInfo ?? "",
        Uri = httpContext.Request.Path,
        Ip = httpContext.Connection?.RemoteIpAddress?.ToString(),
        User = token,
        Method = httpContext.Request.Method ?? "",
        Input = bodyContent,
      };

      await _db.ActionLogs!.AddAsync(actionLog);
      await _db.SaveChangesAsync();

      return _apiResult.HttpOk();
    }
  }
}
