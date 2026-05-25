/// リスト8-4 Repositoryインターフェイスの実装
/// データを見に行く(取得)
/// データアクセス処理の抽象化
/// DbContextやORMへの直接依存を隠蔽
/// 永続化ロジック（CRUD操作）を集約
/// ドメイン層とデータアクセス層の分離
/// 
using WebApp_Src.Infrastructures.Context;
using WebApp_Src.Applications.Domains;
using WebApp_Src.Applications.Repositories;
using WebApp_Src.Infrastructures.Adapters;
using WebApp_Src.Exceptions;
namespace WebApp_Src.Infrastructures.Repositories;
/// <summary>
/// ドメインオブジェクト:社員のCRUD操作インターフェイスの実装
/// </summary>
public class EmployeeRepository : IEmployeeRepository
{
    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    /// ドメインモデル:社員と社員エンティティの相互変換インターフェイスの実装
    /// </summary>
    private readonly EmployeeEntityAdapter _adapter;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context"></param>
    /// <param name="adapter"></param>
    public EmployeeRepository(AppDbContext context, EmployeeEntityAdapter adapter)
    {
        _context = context;
        _adapter = adapter;
    }

    /// <summary>
    /// 社員を永続化する
    /// </summary>
    /// <param name="employee">永続化対象の社員</param>
    public void Create(Employee employee)
    {
        try
        {
            var entity = _adapter.Convert(employee);
            _context.Employees.Add(entity);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new InternalException(
                "社員の永続化ができませんでした。", e);
        }
    }
    public Employee? FindByMail(string mail)
    {
        var result = _context.Employees.FirstOrDefault(e => e.EmpMailadress == mail);
        if (result == null)
        {
            return null;
        }
        return _adapter.Restore(result);
    }
    public Employee? FindByPhone(string phone)
    {
        var result = _context.Employees.FirstOrDefault(e => e.EmpPhonenumber == phone);
        if (result == null)
        {
            return null;
        }
        return _adapter.Restore(result);
    }
    public List<Employee> GetAll()
    {
        try
        {
            var entities = _context.Employees.ToList();
            var results = new List<Employee>();
            foreach (var entity in entities)
            {
                results.Add(_adapter.Restore(entity));
            }
            return results;
        }
        catch (Exception e)
        {
            throw new InternalException(
                "すべての社員を取得できませんでした。", e);
        }
    }
}