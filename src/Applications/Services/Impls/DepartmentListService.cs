/// Serviceインターフェイスとその実装

using WebApp_Src.Applications.Repositories;
using WebApp_Src.Applications.Domains;
using WebApp_Src.Exceptions;
using WebApp_Src.Infrastructures.Context;
namespace WebApp_Src.Applications.Services.Impls;

public class DepartmentListService : IDepartmentListService
{
    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;

    /// <summary>
    /// ドメインオブジェクト:部署のCRUD操作インターフェイス
    /// </summary>
    private readonly IDepartmentRepository _departmentRepository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DepartmentListService(
        AppDbContext context,
        IDepartmentRepository departmentRepository)
    {
        _context = context;
        _departmentRepository = departmentRepository;
    }
    /// <summary>
    /// すべての部署を取得する
    /// </summary>
    /// <returns></returns>
    public List<Department> GetDepartments()
    {
        return _departmentRepository.GetAll();
    }

}