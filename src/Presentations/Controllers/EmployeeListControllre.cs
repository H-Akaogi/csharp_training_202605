/// 社員一覧コントローラ

using WebApp_Src.Infrastructures.Context;
using Microsoft.AspNetCore.Mvc;
using WebApp_Src.Applications.Services;
using WebApp_Src.Presentations.ViewModels;
namespace WebApp_Src.Presentations.Controllers;
/// <summary>
/// 社員登録コントローラ
/// </summary>
[Route("EmployeeList")]
public class EmployeeListController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<EmployeeListController> _logger;
    /// <summary>
    /// 社員登録サービスインターフェイス
    /// </summary>
    private readonly IEmployeeListService _employeeListService;
    /// <summary>
    /// 社員登録ViewModelをEmployeeに変換するアダプター
    /// </summary>
    private readonly EmployeeListViewModelAdapter _adapter;
    /// <summary>
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    private readonly TempDataStore<EmployeeListViewModel> _empDataStore;
    private readonly AppDbContext _context;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public EmployeeListController(
        ILogger<EmployeeListController> logger,
        IEmployeeListService employeeListService,
        EmployeeListViewModelAdapter employeeListViewModelAdapter,
        TempDataStore<EmployeeListViewModel> empDataStore,
        AppDbContext context)
    {
        _logger = logger;
        _employeeListService = employeeListService;
        _adapter = employeeListViewModelAdapter;
        _empDataStore = empDataStore;
        _context = context;
    }

    /// <summary>
    /// 従業登録(入力)画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("ShowEmp")]
    public IActionResult ShowEmp()
    {
        var viewModel = new EmployeeListViewModel();

        var list = (
            from e in _context.Employees
            join d in _context.Departments
            on e.DeptId equals d.DeptId
            select new EmployeeListViewModel
            {
                EmpName = e.EmpName,
                EmpMailadress = e.EmpMailadress,
                EmpPhonenumber = e.EmpPhonenumber,
                DeptName = d.DeptName
            }
        ).ToList()
        ;

        return View(list);

    }

}