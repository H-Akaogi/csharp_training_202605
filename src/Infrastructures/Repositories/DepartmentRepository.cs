/// リスト8-4 Repositoryインターフェイスの実装
/// データを見に行く(取得)
/// データアクセス処理の抽象化
/// DbContextやORMへの直接依存を隠蔽
/// 永続化ロジック（CRUD操作）を集約
/// ドメイン層とデータアクセス層の分離

using WebApp_Src.Infrastructures.Context;
using WebApp_Src.Applications.Domains;
using WebApp_Src.Applications.Repositories;
using WebApp_Src.Infrastructures.Adapters;
using WebApp_Src.Exceptions;
namespace WebApp_Src.Infrastructures.Repositories;
/// <summary>
/// ドメインオブジェクト:部署のCRUD操作インターフェイス実装
/// </summary>
public class DepartmentRepository : IDepartmentRepository
{
    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    /// ドメインモデル:部署と部署エンティティの相互変換インターフェイスの実装
    /// </summary>
    private readonly DepartmentEntityAdapter _adapter;

    public DepartmentRepository(AppDbContext context, DepartmentEntityAdapter adapter)
    {
        _context = context;
        _adapter = adapter;
    }

    /// <summary>
    /// すべての部署を取得する
    /// </summary>
    /// <returns>部署のリスト</returns>
    public List<Department> FindAll()
    {
        try
        {
            var entities = _context.Departments.ToList();
            var results = new List<Department>();
            foreach (var entity in entities)
            {
                results.Add(_adapter.Restore(entity));
            }
            return results;
        }
        catch (Exception e)
        {
            throw new InternalException(
                "すべての部署を取得できませんでした。", e);
        }
    }

    /// <summary>
    /// 指定された部署Idの部署を取得する
    /// </summary>
    /// <param name="id">部署Id</param>
    /// <returns>取得して部署</returns>
    public Department? FindById(int id)
    {
        try
        {
            var result = _context.Departments.FirstOrDefault(d => d.DeptId == id);
            if (result == null)
            {
                return null;
            }
            return _adapter.Restore(result);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "指定された部署Idの部署を取得できませんでした。", e);
        }
    }
}