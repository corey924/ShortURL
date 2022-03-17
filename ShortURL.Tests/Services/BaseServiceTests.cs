using Microsoft.EntityFrameworkCore;
using ShortURL.Services.Database.Entity;

namespace ShortURL.Tests.Services
{
  public class BaseServiceTests
  {
    internal ShortUrlDbContext Db;

    internal BaseServiceTests()
    {
      static ShortUrlDbContext GetDbContext()
      {
        var options = new DbContextOptionsBuilder<ShortUrlDbContext>()
          .UseInMemoryDatabase(databaseName: "TestDb")
          .Options;
        return new ShortUrlDbContext(options);
      }
      Db = GetDbContext();
    }
  }
}
