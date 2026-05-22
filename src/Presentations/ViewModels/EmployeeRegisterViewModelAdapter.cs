/// ViewModelからドメインオブジェクトに変換するAdapter

using WebApp_Src.Applications.Adapters;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;
/// <summary>
/// EmployeeRegisterViewModel(社員登録ViewModel)を
/// ドメインオブジェクト:Employeeに変換するアダプターインターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Employee</typeparam>
/// <typeparam name="TTarget">EmployeeRegisterForm</typeparam>
public class EmployeeRegisterViewModelAdapter : IRestorer<Employee, EmployeeRegisterViewModel>
{
    /// <summary>
    /// EmployeeRegisterViewModelをドメインオブジェクト:Employeeに変換する
    /// </summary>
    /// <param name="target">EmployeeRegisterViewModel</param>
    /// <returns>ドメインオブジェクト:Employee</returns>
    public Employee Restore(EmployeeRegisterViewModel target)
    {
        // Department(部門)を作成する
        var department = new Department(target.DeptId!.Value, target.DeptName);
        // 登録するEmployee(社員)を作成する
        var employee = new Employee(target.EmpName!, target.EmpMailadress!, target.EmpPhonenumber!, department);
        return employee;
    }
}