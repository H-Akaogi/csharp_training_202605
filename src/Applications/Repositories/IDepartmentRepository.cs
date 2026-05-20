/// Repositoryインターフェイス

using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Applications.Repositories;
/// <summary>
/// ドメインオブジェクト:部署のCRUD操作インターフェイス
/// </summary>
public interface IDepartmentRepository
{
    /// <summary>
    /// すべての部署を取得する
    /// </summary>
    /// <returns>部署のリスト</returns>
    List<Department> FindAll();

    /// <summary>
    /// 指定された部署Idの部署を取得する
    /// </summary>
    /// <param name="id">部署Id</param>
    /// <returns>取得して部署</returns>
    Department? FindById(int id);
}