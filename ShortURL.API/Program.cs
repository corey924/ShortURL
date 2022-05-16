using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using ShortURL.Services.Database.Entity;
using ShortURL.Services.Interfaces;
using ShortURL.Services.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .CreateLogger();

try
{
  Log.Information("Starting web host");

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
    //options.Filters.Add(typeof(ActionLog));
  });

  builder.Host.UseSerilog();
  builder.Services.AddScoped<IApiAuthorize, ApiAuthorizeService>();
  builder.Services.AddScoped<IUrlRedirection, UrlRedirectionService>();
  builder.Services.AddScoped<ILog, LogService>();
  builder.Services.AddMemoryCache();

  var app = builder.Build();

  using var scope = app.Services.CreateScope();
  var db = scope.ServiceProvider.GetRequiredService<ShortUrlDbContext>();
  DbSeeder.SeedDb(db);

  app.UseHttpsRedirection();

  app.UseAuthorization();

  app.UseSerilogRequestLogging(options =>
  {
    // �p�G�n�ۭq�T�����d���榡�A�i�H�ק�o�̡A���ק��ä��|�v�T���c�ưO�����ݩ�
    // options.MessageTemplate = "Handled {RequestPath}";

    // �w�]��X���������Ŭ� Information�A�A�i�H�b���ק�O������
    // options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

    // �A�i�H�q httpContext ���o HttpContext �U�Ҧ��i�H���o����T�I
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
      diagnosticContext.Set("IP", httpContext.Connection?.RemoteIpAddress?.ToString());
      diagnosticContext.Set("Headers-Token", httpContext.Request.Headers["token"]);
    };
  });

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

  return 0;
}
catch (Exception ex)
{
  Log.Fatal(ex, "Host terminated unexpectedly");
  return 1;
}
finally
{
  Log.CloseAndFlush();
}


