/// Serviceインターフェイスとその実装

using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Applications.Services;
/// <summary>
/// 社員登録サービスインターフェイス
/// </summary>
public interface IEmployeeUpdateService
{
    List<Department> GetDepartments();
    Department GetById(int id);
    bool ExistsMail(string mail);
    bool ExistsPhone(string phone);

    void Update(Employee employee);
    Employee UpdateById(int id);
}