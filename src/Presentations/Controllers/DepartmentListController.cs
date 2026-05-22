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
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    //private readonly TempDataStore<DepartmentListViewModel> _depDataStore;//★未作成

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DepartmentListController(
        ILogger<DepartmentListController> logger,
        IDepartmentListService departmentListService,
        DepartmentListViewModelAdapter departmentListViewModelAdapter/*,
        TempDataStore<DepartmentListViewModel> deptDataStore*/)
    {
        _logger = logger;
        _departmentListService = departmentListService;
        _adapter = departmentListViewModelAdapter;
        /*_deptDataStore = deptDataStore*/
        ;
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
    /// 確認画面の[戻る]ボタンクリックアクションメソッド
    /// </summary>
    /// <returns></returns> 
    [HttpPost("Back")]
    public IActionResult Back(DepartmentListViewModel viewModel)
    {
        /*
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // DepartmentListViewModelをシリアライズして、TempDataに保存する
        _deptDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする*/
        return RedirectToAction("Home");
    }

    /// <summary>
    /// 部署一覧を取得してViewModelに設定する(SelectListItem形式)
    /// </summary>
    private void PopulateDepartments(DepartmentListViewModel viewModel)
    {
        // 従業員登録サービスから部署一覧を取得する
        var departments = _departmentListService.GetDepartments();
        // 部署一覧をDepartmentListViewModelに登録する
        viewModel.SetDepartments(departments);
        _logger.LogInformation("部署リストを設定");
    }
}