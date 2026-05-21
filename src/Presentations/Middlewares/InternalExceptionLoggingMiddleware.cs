using WebApp_Src.Exceptions;
namespace WebApp_Src.Presentations.Middlewares;
/// <summary>
/// IngternalExceptionをハンドリングするミドルウェア
/// </summary>
public class InternalExceptionLoggingMiddleware
{
    /// <summary>
    /// 次に処理を渡すデリゲート(Controllerなど)
    /// </summary>
    private readonly RequestDelegate _next;
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<InternalExceptionLoggingMiddleware> _logger;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="next">次に処理を渡すデリゲート(Controllerなど)</param>
    /// <param name="logger">ロガー</param> 
    public InternalExceptionLoggingMiddleware(
        RequestDelegate next,
        ILogger<InternalExceptionLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    /// <summary>
    /// ASP.NET Coreのミドルウェア処理
    /// </summary>
    /// <param name="context">HTTPリクエスト情報</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // 次のミドルウェアまたはControllerへ処理を渡す
            await _next(context);
        }
        catch (InternalException ex)
        {
            // エラーログを出力する
            _logger.LogError(ex, "InternalException が発生しました");

            // レスポンスが未送信の場合のみ処理
            if (!context.Response.HasStarted)
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                // システム停止中画面へ遷移
                context.Response.Redirect("/System/Maintenance");
            }
        }
    }
}