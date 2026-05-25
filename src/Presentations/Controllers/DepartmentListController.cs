/// 部門一覧コントローラ

using Microsoft.AspNetCore.Mvc;
using WebApp_Src.Applications.Services;
using WebApp_Src.Presentations.ViewModels;
namespace WebApp_Src.Presentations.Controllers;

[Route("DepartmentList")]
public class DepartmentListController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<DepartmentListController> _logger;
    /// <summary>
    /// サービスインターフェイス
    /// </summary>
    private readonly IDepartmentListService _departmentListService;
    /// <summary>
    /// ViewModelをEmployeeに変換するアダプター
    /// </summary>
    private readonly DepartmentListViewModelAdapter _adapter;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DepartmentListController(
        ILogger<DepartmentListController> logger,
        IDepartmentListService departmentListService,
        DepartmentListViewModelAdapter departmentListViewModelAdapter)
    {
        _logger = logger;
        _departmentListService = departmentListService;
        _adapter = departmentListViewModelAdapter;
    }

    /// <summary>
    /// 部門一覧画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("ShowDept")]
    public IActionResult ShowDept()
    {
        var viewModel = new DepartmentListViewModel();
        // 部署一覧を取得してViewModelに設定する(SelectListItem形式)
        PopulateDepartments(viewModel);
        // viewModelをviewに渡して画面表示する
        return View(viewModel);
    }
    /// <summary>
    /// 部署一覧を取得してViewModelに設定する(SelectListItem形式)
    /// </summary>
    private void PopulateDepartments(DepartmentListViewModel viewModel)
    {
        // 社員登録サービスから部署一覧を取得する
        var departments = _departmentListService.GetDepartments();
        // 部署一覧をDepartmentListViewModelに登録する
        viewModel.SetDepartments(departments);
        _logger.LogInformation("部署リストを設定");

        /*あとでやること:以下に変更
        var departments = _departmentListService.GetDepartments();
        viewModel.ListDepartments(departments)
        */
    }
}