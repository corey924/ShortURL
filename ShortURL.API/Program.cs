using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShortURL.API.Filters;
using ShortURL.Services.Database.Entity;
using ShortURL.Services.Interfaces;
using ShortURL.Services.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShortUrlDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("ShortUrlDatabase")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "ShortURL",
    Version = "v1"
  });
  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
  c.IncludeXmlComments(xmlPath);
});
builder.Services.AddControllersWithViews(options =>
{
  options.Filters.Add(typeof(ActionLog));
});


builder.Services.AddScoped<IApiAuthorize, ApiAuthorizeService>();
builder.Services.AddScoped<IUrlRedirection, UrlRedirectionService>();
builder.Services.AddScoped<ILog, LogService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ShortUrlDbContext>();
DbSeeder.SeedDb(db);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShortURL API");
  });
}

if (app.Environment.IsProduction())
{
  app.UseHttpsRedirection();
}

app.Run();

