/// Serviceインターフェイスと実装クラスを作成する

namespace WebApp_Src.Exceptions;
/// <summary>
/// 該当するデータが見つからないことを表す例外クラス
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message)
    : base(message) { }
    public NotFoundException(string message, Exception innerException)
    : base(message, innerException) { }
}