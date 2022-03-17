using System.ComponentModel.DataAnnotations;

namespace ShortURL.Services.Database.Models
{
  public class ApiAuthorize
  {
    [Key]
    public Guid Token { get; set; }

    /// <summary>
    /// 服務名稱
    /// </summary>
    [Required]
    public string? ServiceName { get; set; }

    /// <summary>
    /// 是否啟用
    /// </summary>
    public bool Enable { get; set; } = true;

    /// <summary>
    /// 建立時間
    /// </summary>
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
    public DateTime UpdatedAt { get; set; }
  }
}
