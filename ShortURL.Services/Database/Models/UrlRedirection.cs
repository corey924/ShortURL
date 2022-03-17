using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShortURL.Services.Database.Models
{
  [Index(nameof(Code), IsUnique = true)]
  public class UrlRedirection
  {
    public UrlRedirection(string code, string url)
    {
      Code = code;
      Url = url;
    }

    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 縮網址簡易代碼
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 欲導向網站
    /// </summary>
    public string Url { get; set; }

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
