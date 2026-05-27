
using WebApp_Src.Applications.Repositories;
using WebApp_Src.Applications.Domains;
using WebApp_Src.Exceptions;
using WebApp_Src.Infrastructures.Context;
namespace WebApp_Src.Applications.Services.Impls;

public class DeleteDepartmentService : IDeleteDepartmentService
{
    private readonly AppDbContext _context;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IEmployeeRepository _employeeRepository;
    public DeleteDepartmentService(
        AppDbContext context,
        IDepartmentRepository departmentRepository,
        IEmployeeRepository employeeRepository)
    {
        _context = context;
        _departmentRepository = departmentRepository;
        _employeeRepository = employeeRepository;
    }

    public Department GetById(int id)
    {
        var result = _departmentRepository.FindById(id)!;
        if (result == null)
        {
            throw new NotFoundException($"部門Id{id}に該当する部門は存在しません");
        }
        return result;
    }
    public void Delete(int id)
    {
        try
        {
            // トランザクションの開始
            _context.Database.BeginTransaction();
            // 社員の登録
            _departmentRepository.DeleteById(id);
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
    public bool ExistEmployee(int id)
    {
        if (_employeeRepository.FindByDepartmentId(id) != true)
        {
            return true;
        }
        else { return false; }
    }
}