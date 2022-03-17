using Microsoft.EntityFrameworkCore;
using ShortURL.Services.Database.Models;

namespace ShortURL.Services.Database.Entity
{
  public class ShortUrlDbContext : DbContext
  {
    public ShortUrlDbContext(DbContextOptions<ShortUrlDbContext> options) : base(options)
    {

    }
    private void RenewMacroColumn()
    {
      var systemDateTime = DateTime.Now;
      foreach (var dbEntry in ChangeTracker.Entries())
      {
        switch (dbEntry.State)
        {
          case EntityState.Added:

            CreateWithValues(dbEntry, "Gid", Guid.NewGuid());
            CreateWithValues(dbEntry, "CreatedAt", systemDateTime);
            CreateWithValues(dbEntry, "UpdatedAt", systemDateTime);
            break;

          case EntityState.Modified:
            CreateWithValues(dbEntry, "UpdatedAt", systemDateTime);
            break;
        }
      }
    }

    private static void CreateWithValues(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry dbEntry, string propertyName, object value)
    {
      try
      {
        if (dbEntry.Property(propertyName) != null)
        {
          dbEntry.Property(propertyName).CurrentValue = value;
        }
      }
      // ReSharper disable once EmptyGeneralCatchClause
      catch { }
    }

    public override int SaveChanges()
    {
      RenewMacroColumn();
      return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      RenewMacroColumn();
      return (await base.SaveChangesAsync(true, cancellationToken));
    }

    #region DbSet

    public virtual DbSet<ApiAuthorize>? ApiAuthorize { get; set; }
    public virtual DbSet<UrlRedirection>? UrlRedirection { get; set; }
    public virtual DbSet<UrlRedirectionLogs>? UrlRedirectionLogs { get; set; }
    public virtual DbSet<ActionLogs>? ActionLogs { get; set; }

    #endregion
  }
}
