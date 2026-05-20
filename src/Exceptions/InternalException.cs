namespace WebApp_Sample.Exceptions;
/// <summary>
/// 内部エラーを表す例外クラス
/// データベース停止や不正なデータアクセスなど
/// </summary>
public class InternalException : Exception
{
    public InternalException(string message)
    : base(message) { }
    public InternalException(string message, Exception innerException)
    : base(message, innerException) { }
}