/// Repositoryインターフェイス

using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Applications.Repositories;
/// <summary>
/// ドメインオブジェクト:社員のCRUD操作インターフェイス
/// </summary>
public interface IEmployeeRepository
{
    /// <summary>
    /// すべての社員を取得する
    /// </summary>
    List<Employee> GetAll();
    /// <summary>
    /// 社員を永続化する
    /// </summary>
    /// <param name="employee">永続化対象の社員</param>
    void Create(Employee employee);
    Employee? FindByMail(string mail);
    Employee? FindByPhone(string phone);
    Employee? DeleteById(int id);
    Employee? FindById(int id);
}