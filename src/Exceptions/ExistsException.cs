namespace WebApp_Src.Exceptions;
/// <summary>
/// 該当するデータがすでにあることを表す例外クラス
/// </summary>
public class ExistsException : Exception
{
    public ExistsException(string message)
    : base(message) { }
    public ExistsException(string message, Exception innerException)
    : base(message, innerException) { }
}