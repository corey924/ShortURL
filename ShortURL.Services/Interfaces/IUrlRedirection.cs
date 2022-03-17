using ShortURL.Services.Commons;
using ShortURL.Services.Database.Models;
using ShortURL.Services.Dto;

namespace ShortURL.Services.Interfaces
{
  public interface IUrlRedirection
  {
    /// <summary>
    /// 建立縮網址內容
    /// </summary>
    /// <param name="urlRedirection"></param>
    /// <returns></returns>
    Task<ApiResponse> CreateUrlRedirectionAsync(CreateUrlRedirectionDto urlRedirection);

    /// <summary>
    /// 更新縮網址內容
    /// </summary>
    /// <param name="urlRedirection"></param>
    /// <returns></returns>
    Task<ApiResponse> UpdateUrlRedirectionAsync(UpdateUrlRedirectionDto urlRedirection);

    /// <summary>
    /// 取得縮網址內容
    /// </summary>
    /// <param name="urlRedirection"></param>
    /// <returns></returns>
    Task<UrlRedirection?> GetUrlRedirectionAsync(GetUrlRedirectionDto urlRedirection);
  }
}
