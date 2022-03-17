using Microsoft.AspNetCore.Mvc.Filters;
using ShortURL.Services.Interfaces;

namespace ShortURL.API.Filters
{
  /// <summary>
  /// 行為紀錄
  /// </summary>
  public class ActionLog : IAsyncResourceFilter
  {
    private readonly ILog _logService;

    /// <summary>
    /// 行為紀錄
    /// </summary>
    /// <param name="logService"></param>
    public ActionLog(ILog logService)
    {
      _logService = logService;
    }

    /// <summary>
    /// 行為紀錄
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
      await _logService.ActionWriteLogAsync(context.HttpContext, await ReadRequestBodyAsync(context.HttpContext));
      var resultContext = await next();
    }

    private async Task<string> ReadRequestBodyAsync(HttpContext context)
    {
      context.Request.EnableBuffering();
      // 注意！要設定 leaveOpen 屬性為 true 使 StreamReader 關閉時，HTTP Request 的 Stream 不會跟著關閉
      string bodyContent = await new StreamReader(context.Request.Body).ReadToEndAsync();
      // 將 HTTP Request 的 Stream 起始位置歸零
      context.Request.Body.Position = 0;
      return bodyContent;
    }
  }
}
