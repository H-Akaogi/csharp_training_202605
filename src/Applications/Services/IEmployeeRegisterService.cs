/// Serviceインターフェイスとその実装

using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Applications.Services;
/// <summary>
/// 社員登録サービスインターフェイス
/// </summary>
public interface IEmployeeRegisterService
{
    /// <summary>
    /// すべての部門を取得する
    /// </summary>
    /// <returns></returns>
    List<Department> GetDepartments();

    /// <summary>
    /// 指定された部門Idの部門を取得する
    /// </summary>
    /// <param name="id">部門Id</param>
    /// <returns></returns>
    Department GetById(int id);
    /// <summary>
    /// 重複確認
    /// </summary>
    bool ExistsMail(string mail);
    bool ExistsPhone(string phone);

    /// <summary>
    /// 新しい社員を登録する
    /// </summary>
    /// <param name="employee"></param>
    void Register(Employee employee);
}