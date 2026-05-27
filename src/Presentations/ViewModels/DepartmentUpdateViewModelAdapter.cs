using WebApp_Src.Applications.Adapters;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;

public class DepartmentUpdateViewModelAdapter : IRestorer<Department, DepartmentUpdateViewModel>
{
    public Department Restore(DepartmentUpdateViewModel target)
    {
        // Department(部門)を作成する
        var department = new Department(target.DeptId, target.DeptName);
        return department;
    }
}