using ShortURL.Services.Database.Models;

namespace ShortURL.Services.Interfaces
{
  public interface IApiAuthorize
  {
    /// <summary>
    /// 確認 Token 是否有效
    /// </summary>
    /// <param name="apiToken"></param>
    /// <returns></returns>
    ApiAuthorize? CheckApiToken(string apiToken);
  }
}
