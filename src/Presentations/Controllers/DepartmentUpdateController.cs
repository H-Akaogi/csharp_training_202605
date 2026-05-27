using WebApp_Src.Exceptions;
using WebApp_Src.Applications.Services.Impls;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebApp_Src.Applications.Services;
using WebApp_Src.Presentations.ViewModels;
using System.ComponentModel.Design;
namespace WebApp_Src.Presentations.Controllers;

[Route("DepartmentUpdate")]
public class DepartmentUpdateController : Controller
{
    private readonly ILogger<DepartmentUpdateController> _logger;
    private readonly IDepartmentUpdateService _departmentUpdateService;
    private readonly DepartmentUpdateViewModelAdapter _adapter;
    private readonly TempDataStore<DepartmentUpdateViewModel> _deptDataStore;

    public DepartmentUpdateController(
     ILogger<DepartmentUpdateController> logger,
     IDepartmentUpdateService departmentUpdateService,
     DepartmentUpdateViewModelAdapter departmentUpdateViewModelAdapter,
     TempDataStore<DepartmentUpdateViewModel> deptDataStore)
    {
        _logger = logger;
        _departmentUpdateService = departmentUpdateService;
        _adapter = departmentUpdateViewModelAdapter;
        _deptDataStore = deptDataStore;
    }
    [HttpGet("Enter/{id}")]
    public IActionResult Enter(int id)
    {
        _logger.LogInformation($"Enter:{id}");
        var department = _departmentUpdateService.UpdateById(id);
        var viewModel = new DepartmentUpdateViewModel
        {
            DeptId = id,
            DeptName = department?.Name
        };
        _logger.LogInformation($"Enter: {id}", viewModel!.ToString());
        _logger.LogInformation($"Enter/ViewModel:{id}");
        return View(viewModel);

    }

    /// <summary>
    /// 入力画面の[完了]ボタンクリックアクションメソッド
    /// </summary>

    /// <summary>
    /// 確認画面の[登録]ボタンクリックアクションメソッド
    /// </summary>
    [HttpPost("Update")]
    public IActionResult Update(DepartmentUpdateViewModel viewModel)
    {
        _logger.LogInformation($"Update:{viewModel.DeptId}");
        // バリデーションチェック
        if (!ModelState.IsValid) // バリデーションエラーあり
        {
            // 入力画面の表示
            return View("Enter", viewModel);
        }
        if (_departmentUpdateService.Exists(viewModel.DeptName!))
        {
            ModelState.AddModelError(nameof(viewModel.DeptName),
            $"{viewModel.DeptName}は既に存在します");
            return View("Enter", viewModel);
        }
        // EmployeeRegisterViewModelをシリアライズして、TempDataに保存する
        _logger.LogInformation($"Update/beforeSave:{viewModel.DeptId}");
        _deptDataStore.Save(this, viewModel);
        // 登録処理GETアクションメソッドにリダイレクトする
        return View(viewModel);
    }


    [HttpPost("Complete")]
    public IActionResult Complete()
    {
        // TempDataからEmployeeRegisterViewModelを取得する
        var viewModel = _deptDataStore.Load(this);
        _logger.LogInformation($"Complete/Load:{viewModel?.DeptId}");
        if (viewModel == null)
        {
            // データが存在しない場合、入力画面にリダイレクト
            return RedirectToAction("ShowDept");
        }
        // EmployeeRegisterFormをドメインモデル:Employeeに変換する
        var department = _adapter.Restore(viewModel!);
        _logger.LogInformation($"Complete/.Restore:{viewModel?.DeptId}");
        // 新しい社員を登録する
        _departmentUpdateService.Update(department);
        _logger.LogInformation($"Complete/.Update:{department?.Id}");
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[戻る]ボタンクリックアクションメソッド
    /// </summary>
    /// <returns></returns> 
    [HttpPost("Back")]
    public IActionResult Back(DepartmentUpdateViewModel viewModel)
    {
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // EmployeeRegisterViewModelをシリアライズして、TempDataに保存する
        _deptDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする
        return RedirectToAction("Enter");
    }
}