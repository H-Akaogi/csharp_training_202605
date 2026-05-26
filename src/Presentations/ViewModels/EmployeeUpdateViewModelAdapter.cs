using WebApp_Src.Applications.Adapters;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;

public class EmployeeUpdateViewModelAdapter : IRestorer<Employee, EmployeeUpdateViewModel>
{
    public Employee Restore(EmployeeUpdateViewModel target)
    {
        // Department(部門)を作成する
        var department = new Department(target.DeptId!.Value, target.DeptName);
        // 登録するEmployee(社員)を作成する
        var employee = new Employee(target.EmpName!, target.EmpMailadress!, target.EmpPhonenumber!, department);
        return employee;
    }
}