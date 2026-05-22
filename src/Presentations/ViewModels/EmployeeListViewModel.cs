/// ViewModel
///  ViewModelは 、画面のために用意する専用のクラス
/// 画面に表示する値や、画面から入力された値をまとめて扱う

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;
/// <summary>
/// ViewModelクラス
/// </summary>
public class EmployeeListViewModel
{
    public int EmpId { get; set; }
    public string? EmpName { get; set; }
    public string? EmpMailadress { get; set; }
    public string? EmpPhonenumber { get; set; }
    public int DeptId { get; set; }
    public string? DeptName { get; set; }

    /// <summary>
    /// 部門のリストをSelectListItemのリストに変換してプロパティに設定する
    /// </summary>
    public void SetEmployees(List<Employee> employees)
    {
        // SelectListItemのリストを作成
        var selectItems = new List<SelectListItem>();

        foreach (var emp in employees)
        {
            if (emp.EmpId.HasValue)
            {
                var item = new SelectListItem();
                item.Value = emp.EmpId.Value.ToString();
                item.Text = string.IsNullOrEmpty(emp.EmpName) ? "(名称未設定)" : emp.EmpName;
                item.Text = string.IsNullOrEmpty(emp.EmpMailadress) ? "(名称未設定)" : emp.EmpMailadress;
                item.Text = string.IsNullOrEmpty(emp.EmpPhonenumber) ? "(名称未設定)" : emp.EmpPhonenumber;
                selectItems.Add(item);
            }
        }
        Employees = selectItems;
    }
    public void SetDepartments(List<Department> departments)
    {
        // SelectListItemのリストを作成
        var selectItems = new List<SelectListItem>();
        foreach (var dept in departments)
        {
            if (dept.Id.HasValue)
            {
                var item = new SelectListItem();
                item.Value = dept.Id.Value.ToString();
                item.Text = string.IsNullOrEmpty(dept.Name) ? "(名称未設定)" : dept.Name;
                selectItems.Add(item);
            }
        }
        Employees = selectItems;
    }
    // 部門のリスト
    public List<SelectListItem>? Employees { get; set; } = null;

}