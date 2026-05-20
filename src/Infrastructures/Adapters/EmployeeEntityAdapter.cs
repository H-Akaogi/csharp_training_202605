/// Adapterインターフェイスの実装
/// (1) ドメインオブジェクト→Entity
/// (2) Entity→ドメインオブジェクト
/// Repository⇔Entity間の変換操作
/// 
using WebApp_Src.Applications.Adapters;
using WebApp_Src.Applications.Domains;
using WebApp_Src.Infrastructures.Entities;
namespace WebApp_Src.Infrastructures.Adapters;
/// <summary>
/// ドメインオブジェクト:EmployeeとEmployeeEntityの相互変換インターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Employee</typeparam>
/// <typeparam name="TTarget">EmployeeEntity</typeparam>
public class EmployeeEntityAdapter :
IConverter<Employee, EmployeeEntity>, IRestorer<Employee, EmployeeEntity>
{
    /// <summary>
    /// ドメインオブジェクト:EmployeeをEmployeeEntityに変換する
    /// </summary>
    /// <param name="domain">ドメインモデル:従業員</param>
    /// <returns>EmployeeEntity</returns>
    public EmployeeEntity Convert(Employee domain)
    {
        var entity = new EmployeeEntity
        {
            EmpName = domain.Name,
            EmpMailAdress = domain.Mailadress,
            EmpPhoneNumber = domain.Phonenumber
        };
        if (domain.Id != null)
        {
            entity.EmpId = domain.Id.Value;
        }
        if (domain.Department != null)
        {
            entity.DeptId = domain.Department.Id;
        }
        return entity;
    }

    /// <summary>
    /// EmployeeEntityからドメインオブジェクト:Employeeを復元する
    /// </summary>
    /// <param name="target">EmployeeEntity</param>
    /// <returns>ドメインオブジェクト:Employee</returns>
    public Employee Restore(EmployeeEntity target)
    {
        var employee = new Employee(
            target.EmpId,
            target.EmpName,
            target.EmpMailAdress,
            target.EmpPhoneNumber,
            null
        );
        return employee;
    }
}