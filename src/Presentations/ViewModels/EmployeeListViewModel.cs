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
    public int? EmpId { get; set; }
    public string? EmpName { get; set; }
    public string? EmpMailadress { get; set; }
    public string? EmpPhonenumber { get; set; }
    public int DeptId { get; set; }
    public string? DeptName { get; set; }
}