using Microsoft.EntityFrameworkCore;
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

    public UrlRedirectionService(ShortUrlDbContext db) : base(db)
    {
      this._db = db;
    }

    public async Task<ApiResponse> CreateUrlRedirectionAsync(CreateUrlRedirectionDto urlRedirection)
    {
      if (!CheckUrl(urlRedirection.ToUrl)) return _apiResult.HttpBadRequest();
      var ranCode = GetRandomAlphanumeric(5);
      while (await _db.UrlRedirection!.AsNoTracking().AnyAsync(x => x.Code == ranCode))
      {
        ranCode = GetRandomAlphanumeric(5);
      }
      var newUrlRedirection = new UrlRedirection(ranCode, urlRedirection.ToUrl!);
      await _db.UrlRedirection!.AddAsync(newUrlRedirection);
      switch (await _db.SaveChangesAsync())
      {
        case 1:
        case 0:
          return _apiResult.HttpCreated(newUrlRedirection);
        default:
          return _apiResult.HttpBadRequest();
      }
    }

    public async Task<UrlRedirection?> GetUrlRedirectionAsync(GetUrlRedirectionDto urlRedirection)
    {
      var getUrlRedirection = await _db.UrlRedirection!.AsNoTracking().FirstOrDefaultAsync(x =>
        x.Enable && x.Code.Equals(urlRedirection.Code));
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
          return _apiResult.HttpOk(updateUrlRedirection);
        default:
          return _apiResult.HttpBadRequest();
      }
    }

    private string GetRandomAlphanumeric(int length)
    {
      var random = new Random();
      const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      return new string(Enumerable.Repeat(characters, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private bool CheckUrl(string? url)
    {
      if (string.IsNullOrWhiteSpace(url)) return false;
      var urlReg = @"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?";
      return System.Text.RegularExpressions.Regex.IsMatch(url, urlReg);
    }
  }
}
