/// Serviceインターフェイスとその実装

using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Applications.Services;

public interface IDepartmentListService
{
    /// <summary>
    /// すべての部門を取得する
    /// </summary>
    /// <returns></returns>
    List<Department> GetDepartments();
}