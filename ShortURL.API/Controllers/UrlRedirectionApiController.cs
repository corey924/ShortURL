using Microsoft.AspNetCore.Mvc;
using ShortURL.Services.Database.Models;
using ShortURL.Services.Dto;
using ShortURL.Services.Interfaces;
using ApiAuthorize = ShortURL.API.Filters.ApiAuthorize;

namespace ShortURL.API.Controllers
{
  /// <summary>
  /// 網址導向
  /// </summary>
  [Route("api/urlRedirection")]
  [ApiController]
  public class UrlRedirectionApiController : ControllerBase
  {
    private readonly IUrlRedirection _urlRedirectionService;
    private readonly ILog _logService;

    /// <summary>
    /// 網址導向
    /// </summary>
    /// <param name="urlRedirectionService"></param>
    /// <param name="logService"></param>
    public UrlRedirectionApiController(IUrlRedirection urlRedirectionService, ILog logService)
    {
      _urlRedirectionService = urlRedirectionService;
      _logService = logService;
    }

    /// <summary>
    /// 執行縮網址導向
    /// </summary>
    /// <param name="id">隨機代碼</param>
    /// <returns></returns>
    [HttpGet, Route("~/{id}")]
    public async Task<IActionResult> GetUrlRedirection(string id)
    {
      var dto = new GetUrlRedirectionDto
      {
        Code = id
      };

      var result = await _urlRedirectionService.GetUrlRedirectionAsync(dto);
      await _logService.UrlRedirectionWriteLogAsync(HttpContext, result ?? new UrlRedirection(id, ""));
      if (result == null) return BadRequest();

      return Redirect(result.Url);
    }

    /// <summary>
    /// 建立縮網址內容
    /// </summary>
    /// <param name="urlRedirection">縮網址內容</param>
    /// <returns></returns>
    [TypeFilter(typeof(ApiAuthorize))]
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UrlRedirection), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUrlRedirection([FromBody] CreateUrlRedirectionDto urlRedirection)
    {
      var result = await _urlRedirectionService.CreateUrlRedirectionAsync(urlRedirection);
      return Ok(result);
    }

    /// <summary>
    /// 更新縮網址內容
    /// </summary>
    /// <param name="urlRedirection">縮網址內容</param>
    /// <returns></returns>
    [TypeFilter(typeof(ApiAuthorize))]
    [HttpPatch]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UrlRedirection), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateUrlRedirection([FromBody] UpdateUrlRedirectionDto urlRedirection)
    {
      var result = await _urlRedirectionService.UpdateUrlRedirectionAsync(urlRedirection);
      return Ok(result);
    }

  }
}
