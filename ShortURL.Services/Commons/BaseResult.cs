using Microsoft.AspNetCore.Http;
using ShortURL.Services.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShortURL.Services.Commons
{
  public class ApiResponse
  {
    public int StatusCode { get; set; }
    public object? Contents { get; set; }
    public string Message { get; set; } = string.Empty;
  }

  public class ApiResult : IApiResult
  {
    public ApiResponse HttpOk() => new() { Message = string.Empty, StatusCode = StatusCodes.Status204NoContent };
    public ApiResponse HttpOk(string message) => new() { Message = message, StatusCode = StatusCodes.Status200OK };
    public ApiResponse HttpOk(object context)
    {
      var jsonOption = new JsonSerializerOptions
      {
        WriteIndented = false,                                          // 是否開啟自動換行
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,               // 鍵採用小駝峰命名格式
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,   // 值為 Null 時自動忽略
        Converters =                                                    // 自動格式化時間參數
        {
          new DateTimeConverterUsingDateTimeParse()
        }
      };

      // .NET 5.0 Newtonsoft.Json
      //var jsonSettings = new JsonSerializerSettings
      //{
      //  DateFormatString = "yyyy-MM-dd HH:mm:ss",
      //  NullValueHandling = NullValueHandling.Ignore,
      //  Formatting = Formatting.None,
      //  ContractResolver = new JsonStringNullToEmpty()
      //};

      var result = new ApiResponse
      {
        StatusCode = StatusCodes.Status200OK,
        Contents = JsonSerializer.Serialize(context, jsonOption)
      };

      return result;
    }
    public ApiResponse HttpCreated() => new() { StatusCode = StatusCodes.Status201Created };
    public ApiResponse HttpCreated(string message) => new() { Message = message, StatusCode = StatusCodes.Status201Created };
    public ApiResponse HttpCreated(object context) => new() { Contents = context, StatusCode = StatusCodes.Status201Created };
    public ApiResponse HttpBadRequest() => new() { StatusCode = StatusCodes.Status400BadRequest };
    public ApiResponse HttpBadRequest(string message) => new() { Message = message, StatusCode = StatusCodes.Status400BadRequest };
    public ApiResponse HttpUnauthorized() => new() { StatusCode = StatusCodes.Status401Unauthorized };
    public ApiResponse HttpUnauthorized(string message) => new() { Message = message, StatusCode = StatusCodes.Status401Unauthorized };
    public ApiResponse HttpForbidden() => new() { StatusCode = StatusCodes.Status403Forbidden };
  }

  /// <summary>
  /// 時間參數格式化
  /// </summary>
  public class DateTimeConverterUsingDateTimeParse : JsonConverter<DateTime>
  {
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      return DateTime.Parse(reader.GetString() ?? string.Empty);
    }
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
    }
  }
}
