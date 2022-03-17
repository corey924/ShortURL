using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShortURL.Services.Interfaces;

namespace ShortURL.API.Filters
{
  /// <summary>
  /// API來源驗證
  /// </summary>
  public class ApiAuthorize : IAuthorizationFilter
  {
    private readonly IApiAuthorize _apiAuthorize;

    /// <summary>
    /// API來源驗證
    /// </summary>
    /// <param name="apiAuthorize"></param>
    public ApiAuthorize(IApiAuthorize apiAuthorize)
    {
      _apiAuthorize = apiAuthorize;
    }
    /// <summary>
    /// 授權檢查
    /// </summary>
    /// <param name="context"></param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
      var token = context.HttpContext.Request.Headers["token"];
      if (string.IsNullOrWhiteSpace(token))
      {
        context.Result = new BadRequestResult();
        return;
      }
      var checkApiToken = _apiAuthorize?.CheckApiToken(token);
      if (checkApiToken == null)
      {
        context.Result = new UnauthorizedResult();
      }
    }
  }
}
