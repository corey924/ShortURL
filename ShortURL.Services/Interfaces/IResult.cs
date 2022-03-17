using ShortURL.Services.Commons;

namespace ShortURL.Services.Interfaces
{
  public interface IResponseContext { }
  public interface IResult { }
  public interface IApiResult : IResult
  {
    /// <summary>
    /// 成功執行
    /// </summary>
    /// <returns></returns>
    ApiResponse HttpOk();

    /// <summary>
    /// 成功執行
    /// </summary>
    /// <param name="message">提示訊息</param>
    /// <returns></returns>
    ApiResponse HttpOk(string message);

    /// <summary>
    /// 成功執行
    /// </summary>
    /// <param name="context">回傳資料</param>
    /// <returns></returns>
    ApiResponse HttpOk(object context);

    /// <summary>
    /// 成功建立
    /// </summary>
    /// <returns></returns>
    ApiResponse HttpCreated();

    /// <summary>
    /// 成功建立
    /// </summary>
    /// <param name="message">提示訊息</param>
    /// <returns></returns>
    ApiResponse HttpCreated(string message);

    /// <summary>
    /// 成功建立
    /// </summary>
    /// <param name="context">回傳資料</param>
    /// <returns></returns>
    ApiResponse HttpCreated(object context);

    /// <summary>
    /// 客戶端錯誤
    /// </summary>
    /// <returns></returns>
    ApiResponse HttpBadRequest();

    /// <summary>
    /// 客戶端錯誤
    /// </summary>
    /// <param name="message">提示訊息</param>
    /// <returns></returns>
    ApiResponse HttpBadRequest(string message);

    /// <summary>
    /// 使用者驗證失敗
    /// </summary>
    /// <returns></returns>
    ApiResponse HttpUnauthorized();

    /// <summary>
    /// 使用者驗證失敗
    /// </summary>
    /// <param name="message">提示訊息</param>
    /// <returns></returns>
    ApiResponse HttpUnauthorized(string message);

    /// <summary>
    /// 使用者權限不足
    /// </summary>
    /// <returns></returns>
    ApiResponse HttpForbidden();
  }
}
