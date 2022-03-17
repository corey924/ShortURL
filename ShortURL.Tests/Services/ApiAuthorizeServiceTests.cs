using NUnit.Framework;
using ShortURL.Services.Database.Models;
using ShortURL.Services.Services;
using System;

namespace ShortURL.Tests.Services
{
  [TestFixture]
  public class ApiAuthorizeServiceTests : BaseServiceTests
  {
    ApiAuthorizeService? _apiAuthorizeService;

    [SetUp]
    public void Init()
    {
      _apiAuthorizeService = new ApiAuthorizeService(Db);
    }

    [Test]
    public void CheckApiToken_StateUnderTest_ExpectedBehavior()
    {
      // Arrange
      string apiToken = "29cc3e11-b87b-443c-b9fd-365631bb8f59";
      var apiAuthorize = new ApiAuthorize
      {
        ServiceName = "Test",
        Token = new Guid(apiToken)
      };
      Db.ApiAuthorize!.Add(apiAuthorize);
      Db.SaveChanges();

      // Act
      var result = _apiAuthorizeService?.CheckApiToken(apiToken);

      // Assert
      Assert.IsNotNull(result);
    }
  }
}
