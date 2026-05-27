/// Serviceインターフェイスとその実装

using WebApp_Src.Applications.Repositories;
using WebApp_Src.Applications.Domains;
using WebApp_Src.Exceptions;
using WebApp_Src.Infrastructures.Context;
namespace WebApp_Src.Applications.Services.Impls;

public class DepartmentUpdateService : IDepartmentUpdateService
{
    private readonly AppDbContext _context;
    private readonly IDepartmentRepository _departmentRepository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DepartmentUpdateService(
        AppDbContext context,
        IDepartmentRepository departmentRepository)
    {
        _context = context;
        _departmentRepository = departmentRepository;
    }
    public Department UpdateById(int id)
    {
        var result = _departmentRepository.FindById(id)!;
        if (result == null)
        {
            throw new NotFoundException($"部門Id{id}に該当する部門は存在しません");
        }
        return result;
    }
    public void Update(Department department)
    {
        try
        {
            Console.WriteLine($"Service/Update/first: {department.Id}");
            // トランザクションの開始
            _context.Database.BeginTransaction();
            //Exists(department.Name!);
            // 部門の登録
            _departmentRepository.Update(department);
            Console.WriteLine($"Service/Update: {department.Id}");
            // トランザクションのコミット
            _context.Database.CommitTransaction();
        }
        catch
        {
            // トランザクションのロールバック
            _context.Database.RollbackTransaction();
            throw;
        }
    }
    public bool Exists(string name)
    {
        return _departmentRepository.FindByName(name) != null;
    }
}