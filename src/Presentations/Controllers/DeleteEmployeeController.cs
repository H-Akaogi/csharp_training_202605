using Microsoft.AspNetCore.Mvc;
using WebApp_Src.Applications.Services;
using WebApp_Src.Presentations.ViewModels;
using Microsoft.VisualBasic;
namespace WebApp_Src.Presentations.Controllers;

[Route("DeleteEmployee")]
public class DeleteEmployeeController : Controller
{
    private readonly ILogger<DeleteEmployeeController> _logger;
    private readonly IDeleteEmployeeService _deleteEmployeeService;
    private readonly DeleteEmployeeViewModelAdapter _adapter;
    private readonly TempDataStore<DeleteEmployeeViewModel> _empDataStore;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DeleteEmployeeController(
        ILogger<DeleteEmployeeController> logger,
        IDeleteEmployeeService deleteEmployeeService,
        DeleteEmployeeViewModelAdapter deleteEmployeeViewModelAdapter,
        TempDataStore<DeleteEmployeeViewModel> empDataStore
        )
    {
        _logger = logger;
        _deleteEmployeeService = deleteEmployeeService;
        _adapter = deleteEmployeeViewModelAdapter;
        _empDataStore = empDataStore;
    }
    // 社員リスト横の[削除]ボタンを押下→確認画面
    [HttpGet("Confirm/{id}")]
    public IActionResult Confirm(int id)
    {
        var employee = _deleteEmployeeService.GetById(id);
        var viewModel = new DeleteEmployeeViewModel
        {
            EmpId = id,
            EmpName = employee.EmpName,
            EmpMailadress = employee.EmpMailadress,
            EmpPhonenumber = employee.EmpPhonenumber,
            DeptName = employee.Department?.Name
        };
        return View(viewModel);
    }
    // 確認画面で[削除]ボタンを押下→削除完了画面
    [HttpPost("Delete")]
    public IActionResult Delete(DeleteEmployeeViewModel viewModel)
    {
        _deleteEmployeeService.Delete(viewModel.EmpId);

        _empDataStore.Save(this, viewModel);

        return RedirectToAction("Complete");
    }

    [HttpGet("Complete")]
    public IActionResult Complete()
    {
        var viewModel = _empDataStore.Load(this);

        if (viewModel == null)
            return RedirectToAction("ShowEmp");

        return View(viewModel);
    }


    // 確認画面で[戻る]ボタンを押下
    [HttpPost("Back")]
    public IActionResult Back(DeleteEmployeeViewModel viewModel)
    {
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // EmployeeRegisterViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする
        return RedirectToAction("ShowEmp");
    }
}