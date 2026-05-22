/// ViewModelからドメインオブジェクトに変換するAdapter

using WebApp_Src.Applications.Adapters;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;

public class DepartmentListViewModelAdapter : IRestorer<Department, DepartmentListViewModel>
{
    public Department Restore(DepartmentListViewModel target)
    {
        // Department(部署)を作成する
        var department = new Department(target.DeptId!, target.DeptName);
        return department;
    }
}