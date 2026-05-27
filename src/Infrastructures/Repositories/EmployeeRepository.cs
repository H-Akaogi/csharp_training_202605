/// リスト8-4 Repositoryインターフェイスの実装
/// データを見に行く(取得)
/// データアクセス処理の抽象化
/// DbContextやORMへの直接依存を隠蔽
/// 永続化ロジック（CRUD操作）を集約
/// ドメイン層とデータアクセス層の分離
/// 
using Microsoft.EntityFrameworkCore;
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
    private readonly EmployeeEntityAdapter _employeeAdapter;
    private readonly DepartmentEntityAdapter _departmentAdapter;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context"></param>
    /// <param name="adapter"></param>
    public EmployeeRepository(
    AppDbContext context,
    EmployeeEntityAdapter employeeAdapter,
    DepartmentEntityAdapter departmentAdapter)
    {
        _context = context;
        _employeeAdapter = employeeAdapter;
        _departmentAdapter = departmentAdapter;
    }


    /// <summary>
    /// 社員を永続化する
    /// </summary>
    /// <param name="employee">永続化対象の社員</param>
    public void Create(Employee employee)
    {
        try
        {
            var entity = _employeeAdapter.Convert(employee);
            _context.Employees.Add(entity);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new InternalException(
                "社員の永続化ができませんでした。", e);
        }
    }

    public Employee? DeleteById(int id)
    {
        try
        {
            var entity = _context.Employees.Single(e => e.EmpId == id);
            _context.Employees.Remove(entity);
            _context.SaveChanges();
            return _employeeAdapter.Restore(entity);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "社員の削除ができませんでした。", e);
        }
    }

    public Employee? FindById(int id)
    {
        try
        {
            var result = _context.Employees
            .Include(e => e.Department)
            .FirstOrDefault(d => d.EmpId == id);
            if (result == null)
            {
                return null;
            }
            return _employeeAdapter.Restore(result);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "指定された社員Idの社員を取得できませんでした。", e);
        }
    }
    public Employee? FindByMail(string mail)
    {
        var result = _context.Employees.FirstOrDefault(e => e.EmpMailadress == mail);
        if (result == null)
        {
            return null;
        }
        return _employeeAdapter.Restore(result);
    }
    public Employee? FindByPhone(string phone)
    {
        var result = _context.Employees.FirstOrDefault(e => e.EmpPhonenumber == phone);
        if (result == null)
        {
            return null;
        }
        return _employeeAdapter.Restore(result);
    }
    public List<Employee> GetAll()
    {
        var entities = _context.Employees
            .Include(e => e.Department)
            .ToList();

        return entities
            .Select(e => _employeeAdapter.Restore(e))
            .ToList();
    }

    public void Update(Employee employee)
    {
        try
        {
            var entity = _employeeAdapter.Convert(employee);
            _context.Employees.Update(entity);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new InternalException(
                "社員の永続化ができませんでした。", e);
        }
    }
}

