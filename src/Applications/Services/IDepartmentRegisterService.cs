/// Serviceインターフェイスとその実装

using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Applications.Services;

public interface IDepartmentRegisterService
{
    /// <summary>
    /// すべての部門を取得する
    /// </summary>
    /// <returns></returns>
    List<Department> GetDepartments();
    bool Exists(string name);
    /// <summary>
    /// 新しい部門を登録する
    /// </summary>
    void Register(Department department);
}