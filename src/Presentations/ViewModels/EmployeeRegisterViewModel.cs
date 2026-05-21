/// ViewModel
///  ViewModelは 、画面のために用意する専用のクラス
/// 画面に表示する値や、画面から入力された値をまとめて扱う

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;
/// <summary>
/// 部署登録ViewModelクラス
/// </summary>
public class EmployeeRegisterViewModel
{
    /// <summary>
    /// 氏名
    /// </summary>
    [Display(Name = "氏名")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    public string? Name { get; set; } = string.Empty;
    /// <summary>
    /// メールアドレス
    /// </summary>
    [Display(Name = "メールアドレス")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    public string? Mailadress { get; set; } = string.Empty;
    /// <summary>
    /// 電話番号
    /// </summary>
    [Display(Name = "電話番号")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    public string? Phonenumber { get; set; } = string.Empty;
    /// <summary>
    /// 所属部署
    /// </summary>
    [Display(Name = "所属部署")]
    [Required(ErrorMessage = "{0}は選択必須です。")]
    public int? DeptId { get; set; } = 0;

    /// <summary>
    /// 選択された部署名
    /// </summary>
    [Display(Name = "部署名")]
    public string? DeptName { get; set; } = string.Empty;

    /// <summary>
    /// 部署のリストをSelectListItemのリストに変換してプロパティに設定する
    /// </summary>
    /// <param name="departments"></param>
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
        Departments = selectItems;
    }
    // 部署のリスト
    public List<SelectListItem>? Departments { get; set; } = null;

    public override string ToString()
    {
        return $"Name={Name} , DeptId={DeptId} , DeptName={DeptName} , Departments={Departments}";
    }
}