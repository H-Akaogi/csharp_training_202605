/// ViewModelからドメインオブジェクトに変換するAdapter

using WebApp_Src.Applications.Adapters;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;

public class DeleteEmployeeViewModelAdapter : IRestorer<Employee, DeleteEmployeeViewModel>
{
    public Employee Restore(DeleteEmployeeViewModel target)
    {
        // Department(部門)を作成する
        var department = new Department(target.DeptId!, target.DeptName);
        // 登録するEmployee(社員)を作成する
        var employee = new Employee(target.EmpName!, target.EmpMailadress!, target.EmpPhonenumber!, department);
        return employee;
    }
}