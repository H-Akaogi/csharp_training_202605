using WebApp_Src.Applications.Adapters;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;

public class DeleteDepartmentViewModelAdapter : IRestorer<Department, DeleteDepartmentViewModel>
{
    public Department Restore(DeleteDepartmentViewModel target)
    {
        // Department(部門)を作成する
        var department = new Department(target.DeptId, target.DeptName);
        return department;
    }
}