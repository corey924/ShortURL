using HttpContextMoq;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using ShortURL.Services.Database.Models;
using ShortURL.Services.Services;
using System.Threading.Tasks;

namespace ShortURL.Tests.Services
{
  [TestFixture]
  public class LogServiceTests : BaseServiceTests
  {
    LogService? _logService;

    [SetUp]
    public void Init()
    {
      _logService = new LogService(Db);
    }

    [Test]
    public async Task UrlRedirectionWriteLog_StateUnderTest_ExpectedBehavior()
    {
      // Arrange
      string code = @"1CY96";
      string toUrl = @"https://www.google.com";
      var urlRedirectionData = new UrlRedirection(code, toUrl);
      var httpContextMock = new HttpContextMock();

      // Act
      var result = await _logService!.UrlRedirectionWriteLogAsync(httpContextMock, urlRedirectionData);

      // Assert
      Assert.AreEqual(result.StatusCode, StatusCodes.Status204NoContent);
    }

    [Test]
    public async Task ActionWriteLog_StateUnderTest_ExpectedBehavior()
    {
      // Arrange
      var httpContextMock = new HttpContextMock();

      // Act
      var result = await _logService!.ActionWriteLogAsync(httpContextMock, "");

      // Assert
      Assert.AreEqual(result.StatusCode, StatusCodes.Status204NoContent);
    }
  }
}
