using System.ComponentModel.DataAnnotations;

namespace ShortURL.Services.Database.Models
{
  public class ActionLogs
  {
    /// <summary>
    /// ID
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Useragent
    /// </summary>
    public string? Device { get; set; }

    /// <summary>
    /// 使用的路徑
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// API內容
    /// </summary>
    public string? Input { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    /// 方法
    /// </summary>
    public string? Method { get; set; }

    /// <summary>
    /// 使用者
    /// </summary>
    public string? User { get; set; }

    /// <summary>
    /// 建立日期
    /// </summary>
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
    public DateTime CreatedAt { get; set; }
  }
}
