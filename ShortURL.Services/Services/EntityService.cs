using Microsoft.Extensions.Configuration;
using ShortURL.Services.Commons;
using ShortURL.Services.Database.Entity;

namespace ShortURL.Services.Services
{
  public abstract class EntityService
  {
    protected readonly ShortUrlDbContext _dbContext;
    protected readonly IConfiguration _config;
    protected readonly ApiResult _apiResult;

    protected EntityService(ShortUrlDbContext context)
    {
      var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      _dbContext = context;
      _config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
        .Build();
      _apiResult = new ApiResult();
    }
  }
}
