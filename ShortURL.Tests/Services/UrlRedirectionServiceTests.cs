using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;
using ShortURL.Services.Database.Models;
using ShortURL.Services.Dto;
using ShortURL.Services.Services;
using System.Threading.Tasks;

namespace ShortURL.Tests.Services
{
  [TestFixture]
  public class UrlRedirectionServiceTests : BaseServiceTests
  {
    UrlRedirectionService? _urlRedirectionService;
    IMemoryCache _memoryCache;

    [SetUp]
    public void Init()
    {
      _memoryCache = new MemoryCache(new MemoryCacheOptions());
      _urlRedirectionService = new UrlRedirectionService(Db, _memoryCache);
    }

    [Test]
    public async Task CreateUrlRedirection_StateUnderTest_ExpectedBehavior()
    {
      // Arrange
      var createUrl = new CreateUrlRedirectionDto
      {
        ToUrl = @"https://www.google.com"
      };

      // Act
      var result = await _urlRedirectionService!.CreateUrlRedirectionAsync(createUrl);

      // Assert
      Assert.AreEqual(result.StatusCode, StatusCodes.Status201Created);
    }

    [Test]
    public async Task GetUrlRedirection_StateUnderTest_ExpectedBehavior()
    {
      // Arrange
      string code = @"1CY96";
      string toUrl = @"https://www.google.com";
      var createUrlRedirection = new UrlRedirection(code, toUrl);
      await Db.UrlRedirection!.AddAsync(createUrlRedirection);
      await Db.SaveChangesAsync();

      var getUrl = new GetUrlRedirectionDto
      {
        Code = "1CY96"
      };

      // Act
      var result = await _urlRedirectionService!.GetUrlRedirectionAsync(getUrl);

      // Assert
      Assert.AreEqual(result!.Url, toUrl);
    }

    [Test]
    public async Task UpdateUrlRedirection_StateUnderTest_ExpectedBehavior()
    {
      // Arrange
      string code = @"1CY96";
      string toUrl = @"https://www.google.com";
      var createUrlRedirection = new UrlRedirection(code, toUrl);
      await Db.UrlRedirection!.AddAsync(createUrlRedirection);
      await Db.SaveChangesAsync();

      var updateUrlRedirection = new UpdateUrlRedirectionDto
      {
        Code = code,
        Url = toUrl,
        Enable = false
      };

      // Act
      var result = await _urlRedirectionService!.UpdateUrlRedirectionAsync(updateUrlRedirection);

      // Assert
      Assert.AreEqual(result.StatusCode, StatusCodes.Status200OK);
    }
  }
}
