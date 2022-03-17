using Microsoft.EntityFrameworkCore;
using ShortURL.Services.Database.Entity;
using ShortURL.Services.Database.Models;
using ShortURL.Services.Interfaces;

namespace ShortURL.Services.Services
{
  public class ApiAuthorizeService : EntityService, IApiAuthorize
  {
    private readonly ShortUrlDbContext _db;

    public ApiAuthorizeService(ShortUrlDbContext db) : base(db)
    {
      this._db = db;
    }

    public ApiAuthorize? CheckApiToken(string apiToken)
    {
      if (string.IsNullOrWhiteSpace(apiToken)) return null;

      if (!Guid.TryParse(apiToken, out var token)) return null;

      var checkApi = _db.ApiAuthorize?.AsNoTracking().FirstOrDefault(x =>
        x.Enable &&
        x.Token == token);

      return checkApi;
    }
  }
}
