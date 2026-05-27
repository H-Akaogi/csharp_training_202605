using Microsoft.AspNetCore.Mvc;
using WebApp_Src.Applications.Services;
using WebApp_Src.Presentations.ViewModels;
namespace WebApp_Src.Presentations.Controllers;
/// <summary>
/// 社員登録コントローラ
/// </summary>
[Route("EmployeeUpdate")]
public class EmployeeUpdateController : Controller
{

    private readonly ILogger<EmployeeUpdateController> _logger;
    private readonly IEmployeeUpdateService _employeeUpdateService;
    private readonly EmployeeUpdateViewModelAdapter _adapter;
    private readonly TempDataStore<EmployeeUpdateViewModel> _empDataStore;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public EmployeeUpdateController(
        ILogger<EmployeeUpdateController> logger,
        IEmployeeUpdateService employeeUpdateService,
        EmployeeUpdateViewModelAdapter employeeUpdateViewModelAdapter,
        TempDataStore<EmployeeUpdateViewModel> empDataStore)
    {
        _logger = logger;
        _employeeUpdateService = employeeUpdateService;
        _adapter = employeeUpdateViewModelAdapter;
        _empDataStore = empDataStore;
    }
    [HttpGet("Enter/{id}")]
    public IActionResult Enter(int id)
    {
        var employee = _employeeUpdateService.UpdateById(id);
        var viewModel = new EmployeeUpdateViewModel
        {
            EmpId = id,
            EmpName = employee.EmpName,
            EmpMailadress = employee.EmpMailadress,
            EmpPhonenumber = employee.EmpPhonenumber,
            DeptId = employee.Department?.Id,
            DeptName = employee.Department?.Name
        };
        PopulateDepartments(viewModel);
        viewModel.DeptName = employee.Department?.Name;
        return View(viewModel);
    }

    /// <summary>
    /// 入力画面の[完了]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost("Confirm")]
    public IActionResult Confirm(EmployeeUpdateViewModel viewModel)
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
        var department = _employeeUpdateService.GetById(viewModel.DeptId ?? 0);
        _logger.LogInformation($"部門Id:{viewModel.DeptId ?? 0}の部門を取得する");
        // ViewModelに部門名を設定する
        viewModel.DeptName = department.Name;
        // 確認画面を表示する
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[登録]ボタンクリックアクションメソッド
    /// </summary>
    [HttpPost("Update")]
    public IActionResult Update(EmployeeUpdateViewModel viewModel)
    {
        // EmployeeUpdateViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 登録処理GETアクションメソッドにリダイレクトする
        return RedirectToAction("Complete");
    }


    [HttpGet("Complete")]
    public IActionResult Complete()
    {
        EmployeeUpdateViewModel? viewModel = null;
        // TempDataからEmployeeUpdateViewModelを取得する
        viewModel = _empDataStore.Load(this);
        if (viewModel == null)
        {
            // データが存在しない場合、入力画面にリダイレクト
            return RedirectToAction("ShowEmp");
        }
        // EmployeeUpdateFormをドメインモデル:Employeeに変換する
        var employee = _adapter.Restore(viewModel!);
        // 新しい社員を登録する
        _employeeUpdateService.Update(employee);
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[戻る]ボタンクリックアクションメソッド
    /// </summary>
    /// <returns></returns> 
    [HttpPost("Back")]
    public IActionResult Back(EmployeeUpdateViewModel viewModel)
    {
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // EmployeeUpdateViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする
        return RedirectToAction("Enter");
    }

    /// <summary>
    /// 部門一覧を取得してViewModelに設定する(SelectListItem形式)
    /// </summary>
    private void PopulateDepartments(EmployeeUpdateViewModel viewModel)
    {
        // 社員登録サービスから部門一覧を取得する
        var departments = _employeeUpdateService.GetDepartments();
        // 部門一覧をEmployeeUpdateViewModelに登録する
        viewModel.SetDepartments(departments);
        _logger.LogInformation("部門リストを設定");
    }
}