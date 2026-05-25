/// ViewModelからドメインオブジェクトに変換するAdapter

using WebApp_Src.Applications.Adapters;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;

public class EmployeeListViewModelAdapter : IRestorer<Employee, EmployeeListViewModel>
{
    public Employee Restore(EmployeeListViewModel target)
    {
        // Department(部門)を作成する
        var department = new Department(target.DeptId!, target.DeptName);
        // 登録するEmployee(社員)を作成する
        var employee = new Employee(target.EmpId!, target.EmpName!, target.EmpMailadress!, target.EmpPhonenumber!, department);
        return employee;
    }
}