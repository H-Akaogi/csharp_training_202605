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
    /// <param name="domain">ドメインモデル:社員</param>
    /// <returns>EmployeeEntity</returns>
    public EmployeeEntity Convert(Employee domain)
    {
        var entity = new EmployeeEntity
        {
            EmpName = domain.EmpName,
            EmpMailadress = domain.EmpMailadress,
            EmpPhonenumber = domain.EmpPhonenumber
        };
        if (domain.EmpId != null)
        {
            entity.EmpId = domain.EmpId.Value;
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
        Department? department = null;

        if (target.Department != null)
        {
            department = new Department(
                target.Department.DeptId,
                target.Department.DeptName
            );
        }

        var employee = new Employee(
            target.EmpId,
            target.EmpName,
            target.EmpMailadress,
            target.EmpPhonenumber,
            department
        );

        return employee;
    }
}