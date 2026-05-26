using WebApp_Src.Applications.Repositories;
using WebApp_Src.Applications.Domains;
using WebApp_Src.Exceptions;
using WebApp_Src.Infrastructures.Context;
namespace WebApp_Src.Applications.Services.Impls;
/// <summary>
/// 社員登録サービスインターフェイスの実装
/// </summary>
public class EmployeeUpdateService : IEmployeeUpdateService
{

    private readonly AppDbContext _context;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public EmployeeUpdateService(
        AppDbContext context,
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository)
    {
        _context = context;
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
    }

    /// <summary>
    /// 指定された部門Idの部門を取得する
    /// </summary>
    /// <param name="id">部門Id</param>
    /// <returns></returns>
    public Department GetById(int id)
    {
        var result = _departmentRepository.FindById(id)!;
        if (result == null)
        {
            throw new NotFoundException($"部門Id{id}に該当する部門は存在しません");
        }
        return result;
    }

    /// <summary>
    /// すべての部門を取得する
    /// </summary>
    /// <returns></returns>
    public List<Department> GetDepartments()
    {
        return _departmentRepository.GetAll();
    }

    /// <summary>
    /// 新しい社員を登録する
    /// </summary>
    /// <param name="employee"></param>
    public void Update(Employee employee)
    {
        try
        {
            // トランザクションの開始
            _context.Database.BeginTransaction();
            // 社員の登録
            _employeeRepository.Update(employee);
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
    // 重複確認
    public bool ExistsMail(string mail)
    {
        return _employeeRepository.FindByMail(mail) != null;
    }
    public bool ExistsPhone(string phone)
    {
        return _employeeRepository.FindByPhone(phone) != null;
    }

    public Employee UpdateById(int id)
    {
        var result = _employeeRepository.FindById(id)!;
        if (result == null)
        {
            throw new NotFoundException($"社員Id:{id}に該当する社員は存在しません");
        }
        return result;
    }
}