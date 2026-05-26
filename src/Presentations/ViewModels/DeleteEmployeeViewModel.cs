/// ViewModel
///  ViewModelは 、画面のために用意する専用のクラス
/// 画面に表示する値や、画面から入力された値をまとめて扱う
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;

public class DeleteEmployeeViewModel
{
    public int EmpId { get; set; }
    [Display(Name = "氏名")]
    public string? EmpName { get; set; }
    [Display(Name = "メールアドレス")]
    public string? EmpMailadress { get; set; }
    [Display(Name = "電話番号")]
    public string? EmpPhonenumber { get; set; }
    public int DeptId { get; set; }

    [Display(Name = "所属部門")]
    public string? DeptName { get; set; }

}