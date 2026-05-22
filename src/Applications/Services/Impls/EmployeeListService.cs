/// Serviceインターフェイスとその実装

using WebApp_Src.Applications.Repositories;
using WebApp_Src.Applications.Domains;
using WebApp_Src.Exceptions;
using WebApp_Src.Infrastructures.Context;
namespace WebApp_Src.Applications.Services.Impls;
/// <summary>
/// 社員登録サービスインターフェイスの実装
/// </summary>
public class EmployeeListService : IEmployeeListService
{
    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    /// ドメインオブジェクト:社員のCRUD操作インターフェイス
    /// </summary>
    private readonly IEmployeeRepository _employeeRepository;
    /// <summary>
    /// ドメインオブジェクト:部門のCRUD操作インターフェイス
    /// </summary>
    private readonly IDepartmentRepository _departmentRepository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context">アプリケーション用DbContext</param>
    /// <param name="employeeRepository">社員のCRUD操作インターフェイス</param>
    /// <param name="departmentRepository">部門のCRUD操作インターフェイス</param>
    public EmployeeListService(
        AppDbContext context,
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository)
    {
        _context = context;
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
    }
    /// <summary>
    /// すべての社員を取得する
    /// </summary>
    /// <returns></returns>
    public List<Employee> GetEmployees()
    {
        return _employeeRepository.GetAll();
    }
    /// <summary>
    /// すべての部門を取得する
    /// </summary>
    /// <returns></returns>
    public List<Department> GetDepartments()
    {
        return _departmentRepository.GetAll();
    }
}