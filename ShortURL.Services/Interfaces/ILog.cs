using Microsoft.AspNetCore.Http;
using ShortURL.Services.Commons;
using ShortURL.Services.Database.Models;

namespace ShortURL.Services.Interfaces
{
  public interface ILog
  {
    /// <summary>
    /// 網址導向LOG
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="urlRedirectionData">導向內容</param>
    /// <returns></returns>
    Task<ApiResponse> UrlRedirectionWriteLogAsync(HttpContext httpContext, UrlRedirection urlRedirectionData);

    /// <summary>
    /// 行為紀錄LOG
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="bodyContent">行為內容</param>
    /// <returns></returns>
    Task<ApiResponse> ActionWriteLogAsync(HttpContext httpContext, string bodyContent);
  }
}
