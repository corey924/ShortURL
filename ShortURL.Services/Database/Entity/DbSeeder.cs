using Microsoft.EntityFrameworkCore;
using ShortURL.Services.Database.Models;

namespace ShortURL.Services.Database.Entity
{
  public static class DbSeeder
  {
    public static void SeedDb(ShortUrlDbContext context)
    {
      context.Database.Migrate();
      if (context.ApiAuthorize != null && !context.ApiAuthorize.Any()) SeedAccount(context);
    }

    private static void SeedAccount(ShortUrlDbContext context)
    {
      context.Database.EnsureCreated();
      context.ApiAuthorize?.Add(
        new ApiAuthorize()
        {
          Token = new Guid("29cc3e11-b87b-443c-b9fd-365631bb8f59"),
          ServiceName = "Admin",
        }
      );
      context.SaveChanges();
    }
  }
}
