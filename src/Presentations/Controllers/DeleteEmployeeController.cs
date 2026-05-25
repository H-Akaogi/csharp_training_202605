using WebApp_Src.Infrastructures.Context;
using Microsoft.AspNetCore.Mvc;
using WebApp_Src.Applications.Services;
using WebApp_Src.Presentations.ViewModels;
namespace WebApp_Src.Presentations.Controllers;

[Route("DeleteEmployee")]
public class DeleteEmployeeController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<DeleteEmployeeController> _logger;
    /// <summary>
    /// 社員登録サービスインターフェイス
    /// </summary>
    private readonly IDeleteEmployeeService _deleteEmployeeService;
    /// <summary>
    /// 社員登録ViewModelをEmployeeに変換するアダプター
    /// </summary>
    private readonly DeleteEmployeeViewModelAdapter _adapter;
    /// <summary>
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    private readonly TempDataStore<DeleteEmployeeViewModel> _empDataStore;
    private readonly AppDbContext _context;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DeleteEmployeeController(
        ILogger<DeleteEmployeeController> logger,
        IDeleteEmployeeService deleteEmployeeService,
        DeleteEmployeeViewModelAdapter deleteEmployeeViewModelAdapter,
        TempDataStore<DeleteEmployeeViewModel> empDataStore,
        AppDbContext context)
    {
        _logger = logger;
        _deleteEmployeeService = deleteEmployeeService;
        _adapter = deleteEmployeeViewModelAdapter;
        _empDataStore = empDataStore;
        _context = context;
    }
    [HttpPost("Confirm")]
    public IActionResult Confirm(DeleteEmployeeViewModel viewModel)
    {
        // バリデーションチェック
        if (!ModelState.IsValid) // バリデーションエラーあり
        {
            // 入力画面の表示
            return View("Enter", viewModel);
        }
        // 選択された部門のIdで部門データを取得する
        var employee = _deleteEmployeeService.DeleteById(viewModel.EmpId ?? 0);
        _logger.LogInformation($"部門Id:{viewModel.EmpId ?? 0}の社員を取得する");
        // ViewModelに部門名を設定する
        viewModel.EmpName = employee.EmpName;

        var viewModel = new DeleteEmployeeViewModel();

        var list = (
            from e in _context.Employees
            join d in _context.Departments
            on e.DeptId equals d.DeptId
            select new DeleteEmployeeViewModel
            {
                EmpId = e.EmpId,
                EmpName = e.EmpName,
                EmpMailadress = e.EmpMailadress,
                EmpPhonenumber = e.EmpPhonenumber,
                DeptName = d.DeptName
            }
        ).ToList()
        ;
        // 確認画面を表示する
        return View(viewModel);
    }
    [HttpPost("Delete")]
    public IActionResult Delete(DeleteEmployeeViewModel viewModel)
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
        DeleteEmployeeViewModel? viewModel = null;
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
        _deleteEmployeeService.Delete(employee);
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[戻る]ボタンクリックアクションメソッド
    /// </summary>
    /// <returns></returns> 
    [HttpPost("Back")]
    public IActionResult Back(DeleteEmployeeViewModel viewModel)
    {
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // EmployeeRegisterViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする
        return RedirectToAction("Enter");
    }
}