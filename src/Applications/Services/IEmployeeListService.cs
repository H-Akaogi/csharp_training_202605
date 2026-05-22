/// Serviceインターフェイスとその実装

using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Applications.Services;
/// <summary>
/// 社員登録サービスインターフェイス
/// </summary>
public interface IEmployeeListService
{

    /// <summary>
    /// すべての部門を取得する
    /// </summary>
    /// <returns></returns>
    List<Employee> GetEmployees();
}