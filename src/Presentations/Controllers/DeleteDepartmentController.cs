using Microsoft.AspNetCore.Mvc;
using WebApp_Src.Applications.Services;
using WebApp_Src.Presentations.ViewModels;
using Microsoft.VisualBasic;
namespace WebApp_Src.Presentations.Controllers;

[Route("DeleteDepartment")]
public class DeleteDepartmentController : Controller
{
    private readonly ILogger<DeleteDepartmentController> _logger;
    private readonly IDeleteDepartmentService _deleteDepartmentService;
    private readonly DeleteDepartmentViewModelAdapter _adapter;
    private readonly TempDataStore<DeleteDepartmentViewModel> _deptDataStore;

    public DeleteDepartmentController(
      ILogger<DeleteDepartmentController> logger,
      IDeleteDepartmentService deleteDepartmentService,
      DeleteDepartmentViewModelAdapter deleteDepartmentViewModelAdapter,
      TempDataStore<DeleteDepartmentViewModel> deptDataStore)
    {
        _logger = logger;
        _deleteDepartmentService = deleteDepartmentService;
        _adapter = deleteDepartmentViewModelAdapter;
        _deptDataStore = deptDataStore;
    }
    [HttpGet("Confirm/{id}")]
    public IActionResult Confirm(int id)
    {
        var department = _deleteDepartmentService.GetById(id);
        var viewModel = new DeleteEmployeeViewModel
        {
            DeptName = department?.Name
        };
        return View(viewModel);
    }
}