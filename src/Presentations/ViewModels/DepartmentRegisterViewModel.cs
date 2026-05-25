/// ViewModel
///  ViewModelは 、画面のために用意する専用のクラス
/// 画面に表示する値や、画面から入力された値をまとめて扱う

using System.ComponentModel.DataAnnotations;
using System.Xml.XPath;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;

public class DepartmentRegisterViewModel
{
    /// <summary>
    /// 選択された部門名
    /// </summary>
    [Display(Name = "部門名")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [StringLength(20, ErrorMessage = "部門名は20文字以内で入力してください。")]

    public string? DeptName { get; set; } = string.Empty;
    public override string ToString()
    {
        return $"DeptName={DeptName}";
    }
}