namespace WebApp_Src.Exceptions;
/// <summary>
/// ドメインルール違反を表す例外クラス
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message)
    : base() { }//base(messageじゃないの？)
    public DomainException(string message, Exception innerException)
    : base(message, innerException) { }
}