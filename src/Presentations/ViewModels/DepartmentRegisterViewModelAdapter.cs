/// ViewModelからドメインオブジェクトに変換するAdapter

using WebApp_Src.Applications.Adapters;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;

public class DepartmentRegisterViewModelAdapter : IRestorer<Department, DepartmentRegisterViewModel>
{
    public Department Restore(DepartmentRegisterViewModel target)
    {
        // Department(部門)を作成する
        var department = new Department(target.DeptId!, target.DeptName);
        return department;
    }
}