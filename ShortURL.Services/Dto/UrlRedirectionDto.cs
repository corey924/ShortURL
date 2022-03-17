namespace ShortURL.Services.Dto
{
  public class UpdateUrlRedirectionDto
  {
    /// <summary>
    /// 縮網址簡易代碼
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 欲導向網站
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// 是否啟用
    /// </summary>
    public bool Enable { get; set; }
  }

  public class GetUrlRedirectionDto
  {
    /// <summary>
    /// 縮網址簡易代碼
    /// </summary>
    public string? Code { get; set; }
  }

  public class CreateUrlRedirectionDto
  {
    /// <summary>
    /// 欲導向網站
    /// </summary>
    public string? ToUrl { get; set; }
  }
}
