/// Serviceインターフェイスとその実装

using WebApp_Src.Applications.Repositories;
using WebApp_Src.Applications.Domains;
using WebApp_Src.Exceptions;
using WebApp_Src.Infrastructures.Context;
namespace WebApp_Src.Applications.Services.Impls;
/// <summary>
/// 社員登録サービスインターフェイスの実装
/// </summary>
public class DeleteEmployeeService : IDeleteEmployeeService
{    /// <summary>
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
    public DeleteEmployeeService(
    AppDbContext context,
    IEmployeeRepository employeeRepository,
    IDepartmentRepository departmentRepository)
    {
        _context = context;
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
    }
    public void Delete(int id)
    {
        try
        {
            // トランザクションの開始
            _context.Database.BeginTransaction();
            // 社員の登録
            _employeeRepository.DeleteById(id);
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
}