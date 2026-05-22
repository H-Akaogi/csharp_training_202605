/// 社員登録コントローラ


using Microsoft.AspNetCore.Mvc;
using WebApp_Src.Applications.Services;
using WebApp_Src.Presentations.ViewModels;
namespace WebApp_Src.Presentations.Controllers;
/// <summary>
/// 社員登録コントローラ
/// </summary>
[Route("EmployeeRegister")]
public class EmployeeRegisterController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<EmployeeRegisterController> _logger;
    /// <summary>
    /// 社員登録サービスインターフェイス
    /// </summary>
    private readonly IEmployeeRegisterService _employeeRegisterService;
    /// <summary>
    /// 社員登録ViewModelをEmployeeに変換するアダプター
    /// </summary>
    private readonly EmployeeRegisterViewModelAdapter _adapter;
    /// <summary>
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    private readonly TempDataStore<EmployeeRegisterViewModel> _empDataStore;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    /// <param name="employeeRegisterService">社員登録サービスインターフェイス</param>
    /// <param name="employeeRegisterViewModelAdapter">社員登録ViewModelをEmployeeに変換するアダプター</param>
    /// <param name="empDataStore">TempDataを通じて一時的にViewModelを保存・復元するためのクラス</param>
    public EmployeeRegisterController(
        ILogger<EmployeeRegisterController> logger,
        IEmployeeRegisterService employeeRegisterService,
        EmployeeRegisterViewModelAdapter employeeRegisterViewModelAdapter,
        TempDataStore<EmployeeRegisterViewModel> empDataStore)
    {
        _logger = logger;
        _employeeRegisterService = employeeRegisterService;
        _adapter = employeeRegisterViewModelAdapter;
        _empDataStore = empDataStore;
    }

    /// <summary>
    /// 従業登録(入力)画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Enter")]
    public IActionResult Enter()
    {
        EmployeeRegisterViewModel? viewModel = null;
        // [戻る]ボタンへの対応
        // TempDataからEmployeeRegisterViewModelを取得する
        viewModel = _empDataStore.Load(this);
        if (viewModel == null)
        {
            // 社員登録ViewModelを生成する
            viewModel = new EmployeeRegisterViewModel();
        }
        // 部門一覧を取得してViewModelに設定する(SelectListItem形式)
        PopulateDepartments(viewModel);
        // viewModelをviewに渡して画面表示する
        return View(viewModel);
    }

    /// <summary>
    /// 入力画面の[完了]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost("Confirm")]
    public IActionResult Confirm(EmployeeRegisterViewModel viewModel)
    {
        // バリデーションチェック
        if (!ModelState.IsValid) // バリデーションエラーあり
        {
            // 部門一覧を取得してViewModelに設定する(SelectListItem形式)
            PopulateDepartments(viewModel);
            // 入力画面の表示
            return View("Enter", viewModel);
        }
        // 選択された部門のIdで部門データを取得する
        var department = _employeeRegisterService.GetById(viewModel.DeptId ?? 0);
        _logger.LogInformation($"部門Id:{viewModel.DeptId ?? 0}の部門を取得する");
        // ViewModelに部門名を設定する
        viewModel.DeptName = department.Name;
        // 確認画面を表示する
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[登録]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("Regiter")]
    public IActionResult Register(EmployeeRegisterViewModel viewModel)
    {
        // EmployeeRegisterViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 登録処理GETアクションメソッドにリダイレクトする
        return RedirectToAction("Complete");
    }

    /// <summary>
    /// アクションメソッド:Regiter()のリダイレクト先
    /// PRGパターン
    /// </summary>
    /// <returns></returns>
    [HttpGet("Complete")]
    public IActionResult Complete()
    {
        EmployeeRegisterViewModel? viewModel = null;
        // TempDataからEmployeeRegisterViewModelを取得する
        viewModel = _empDataStore.Load(this);
        if (viewModel == null)
        {
            // データが存在しない場合、入力画面にリダイレクト
            return RedirectToAction("Enter");
        }
        // EmployeeRegisterFormをドメインモデル:Employeeに変換する
        var employee = _adapter.Restore(viewModel!);
        // 新しい社員を登録する
        _employeeRegisterService.Register(employee);
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[戻る]ボタンクリックアクションメソッド
    /// </summary>
    /// <returns></returns> 
    [HttpPost("Back")]
    public IActionResult Back(EmployeeRegisterViewModel viewModel)
    {
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // EmployeeRegisterViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする
        return RedirectToAction("Enter");
    }

    /// <summary>
    /// 部門一覧を取得してViewModelに設定する(SelectListItem形式)
    /// </summary>
    private void PopulateDepartments(EmployeeRegisterViewModel viewModel)
    {
        // 社員登録サービスから部門一覧を取得する
        var departments = _employeeRegisterService.GetDepartments();
        // 部門一覧をEmployeeRegisterViewModelに登録する
        viewModel.SetDepartments(departments);
        _logger.LogInformation("部門リストを設定");
    }
}