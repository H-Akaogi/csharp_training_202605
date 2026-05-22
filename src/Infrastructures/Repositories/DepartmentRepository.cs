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
/// ドメインオブジェクト:部門のCRUD操作インターフェイス実装
/// </summary>
public class DepartmentRepository : IDepartmentRepository
{
    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    /// ドメインモデル:部門と部門エンティティの相互変換インターフェイスの実装
    /// </summary>
    private readonly DepartmentEntityAdapter _adapter;

    public DepartmentRepository(AppDbContext context, DepartmentEntityAdapter adapter)
    {
        _context = context;
        _adapter = adapter;
    }

    /// <summary>
    /// すべての部門を取得する
    /// </summary>
    /// <returns>部門のリスト</returns>
    public List<Department> GetAll()
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
                "すべての部門を取得できませんでした。", e);
        }
    }

    /// <summary>
    /// 指定された部門Idの部門を取得する
    /// </summary>
    /// <param name="id">部門Id</param>
    /// <returns>取得して部門</returns>
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
                "指定された部門Idの部門を取得できませんでした。", e);
        }
    }
    /// <summary>
    /// 部門を永続化する
    /// </summary>
    public void Create(Department department)
    {
        try
        {
            var entity = _adapter.Convert(department);
            _context.Departments.Add(entity);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new InternalException(
                "部門の永続化ができませんでした。", e);
        }
    }
}