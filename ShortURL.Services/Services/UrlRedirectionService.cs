using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ShortURL.Services.Commons;
using ShortURL.Services.Database.Entity;
using ShortURL.Services.Database.Models;
using ShortURL.Services.Dto;
using ShortURL.Services.Interfaces;

namespace ShortURL.Services.Services
{
  public class UrlRedirectionService : EntityService, IUrlRedirection
  {
    private readonly ShortUrlDbContext _db;
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// 隨機代碼位數
    /// </summary>
    private const int codeLength = 5;

    public UrlRedirectionService(ShortUrlDbContext db, IMemoryCache memoryCache) : base(db)
    {
      this._db = db;
      _memoryCache = memoryCache;
    }

    public async Task<ApiResponse> CreateUrlRedirectionAsync(CreateUrlRedirectionDto urlRedirection)
    {
      if (!CheckUrl(urlRedirection.ToUrl)) return _apiResult.HttpBadRequest();
      var ranCode = GetRandomAlphanumeric(codeLength);
      while (await _db.UrlRedirection!.AsNoTracking().AnyAsync(x => x.Code == ranCode))
      {
        ranCode = GetRandomAlphanumeric(codeLength);
      }
      var newUrlRedirection = new UrlRedirection(ranCode, urlRedirection.ToUrl!);
      await _db.UrlRedirection!.AddAsync(newUrlRedirection);
      switch (await _db.SaveChangesAsync())
      {
        case 1:
        case 0:
          SetUrlCache(newUrlRedirection.Code, newUrlRedirection.Url);
          return _apiResult.HttpCreated(newUrlRedirection);
        default:
          return _apiResult.HttpBadRequest();
      }
    }

    public async Task<UrlRedirection?> GetUrlRedirectionAsync(GetUrlRedirectionDto urlRedirection)
    {
      if (string.IsNullOrWhiteSpace(urlRedirection.Code) || urlRedirection.Code.Length != codeLength) return null;
      if (_memoryCache.TryGetValue(urlRedirection.Code!, out string toUrl))
      {
        return new UrlRedirection(urlRedirection.Code!, toUrl);
      }
      var getUrlRedirection = await _db.UrlRedirection!.AsNoTracking().FirstOrDefaultAsync(x =>
        x.Enable && x.Code.Equals(urlRedirection.Code));
      if (getUrlRedirection != null) SetUrlCache(getUrlRedirection?.Code!, getUrlRedirection?.Url!);
      return getUrlRedirection;
    }

    public async Task<ApiResponse> UpdateUrlRedirectionAsync(UpdateUrlRedirectionDto urlRedirection)
    {
      if (!CheckUrl(urlRedirection.Url)) return _apiResult.HttpBadRequest();
      var updateUrlRedirection = await _db.UrlRedirection!.FirstOrDefaultAsync(x =>
        x.Code.Equals(urlRedirection.Code));

      if (updateUrlRedirection == null) return _apiResult.HttpBadRequest();

      updateUrlRedirection.Url = urlRedirection.Url!;
      updateUrlRedirection.Enable = urlRedirection.Enable;

      switch (await _db.SaveChangesAsync())
      {
        case 1:
        case 0:
          if (updateUrlRedirection.Enable)
          {
            SetUrlCache(urlRedirection.Code!, urlRedirection.Url!);
          }
          else
          {
            _memoryCache.Remove(urlRedirection.Code);
          }
          return _apiResult.HttpOk(updateUrlRedirection);
        default:
          return _apiResult.HttpBadRequest();
      }
    }

    /// <summary>
    /// 取得任意位隨機英數
    /// </summary>
    /// <param name="length">位數</param>
    /// <returns></returns>
    private string GetRandomAlphanumeric(int length)
    {
      var random = new Random();
      const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      return new string(Enumerable.Repeat(characters, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// 檢查網址格式
    /// </summary>
    /// <param name="url">網址</param>
    /// <returns></returns>
    private bool CheckUrl(string? url)
    {
      if (string.IsNullOrWhiteSpace(url)) return false;
      var urlReg = @"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?";
      return System.Text.RegularExpressions.Regex.IsMatch(url, urlReg);
    }

    /// <summary>
    /// 設定快取
    /// </summary>
    /// <param name="code">縮網址代碼</param>
    /// <param name="toUrlNew">設定新的導向網址</param>
    /// <returns></returns>
    private string SetUrlCache(string code, string toUrlNew)
    {
      if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(toUrlNew)) return "";
      var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)).SetSize(1024);
      _memoryCache.Set(code, toUrlNew, cacheEntryOptions);
      return toUrlNew;
    }
  }
}
