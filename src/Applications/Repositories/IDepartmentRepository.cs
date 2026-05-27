/// Repositoryインターフェイス

using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Applications.Repositories;
/// <summary>
/// ドメインオブジェクト:部門のCRUD操作インターフェイス
/// </summary>
public interface IDepartmentRepository
{
    /// <summary>
    /// すべての部門を取得する
    /// </summary>
    /// <returns>部門のリスト</returns>
    List<Department> GetAll();

    /// <summary>
    /// 指定された部門Idの部門を取得する
    /// </summary>
    Department? FindById(int id);
    Department? FindByName(string name);
    /// <summary>
    /// 部門を永続化する
    /// </summary>
    void Create(Department department);
    void Update(Department department);
}