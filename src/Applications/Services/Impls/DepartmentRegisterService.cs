/// Serviceインターフェイスとその実装

using WebApp_Src.Applications.Repositories;
using WebApp_Src.Applications.Domains;
using WebApp_Src.Exceptions;
using WebApp_Src.Infrastructures.Context;
namespace WebApp_Src.Applications.Services.Impls;

public class DepartmentRegisterService : IDepartmentRegisterService
{
    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    /// ドメインオブジェクト:部門のCRUD操作インターフェイス
    /// </summary>
    private readonly IDepartmentRepository _departmentRepository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context">アプリケーション用DbContext</param>
    public DepartmentRegisterService(
        AppDbContext context,
        IDepartmentRepository departmentRepository)
    {
        _context = context;
        _departmentRepository = departmentRepository;
    }
    // すべての部門の取得
    public List<Department> GetDepartments()
    {
        return _departmentRepository.GetAll();
    }
    /// <summary>
    /// 新しい部門を登録する
    /// </summary>
    public void Register(Department department)
    {
        //存在する場合のExistsExceptionエラー
        var result = _departmentRepository.GetAll();
        if (result != null)
        {
            throw new ExistsException($"部門名{department}は既に存在します");
        }
        try
        {
            // トランザクションの開始
            _context.Database.BeginTransaction();
            // 部門の登録
            _departmentRepository.Create(department);
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