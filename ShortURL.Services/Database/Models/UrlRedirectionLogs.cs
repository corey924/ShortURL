using System.ComponentModel.DataAnnotations;

namespace ShortURL.Services.Database.Models
{
  public class UrlRedirectionLogs
  {
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 轉址代碼
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 導向網址
    /// </summary>
    public string? ToUrl { get; set; }

    /// <summary>
    /// 使用者IP
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// 裝置資訊
    /// </summary>
    public string? DeviceInfo { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
    public DateTime CreatedAt { get; set; }
  }
}
