using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;

public class EmployeeUpdateViewModel
{
    public int EmpId { get; set; }
    /// <summary>
    /// 氏名
    /// </summary>
    [Display(Name = "氏名")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [StringLength(20, ErrorMessage = "氏名は20文字以内で入力してください。")]
    public string? EmpName { get; set; } = string.Empty;

    /// <summary>
    /// メールアドレス
    /// </summary>
    [Display(Name = "メールアドレス")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [EmailAddress(ErrorMessage = "メールアドレスの形式で入力してください。")]
    [StringLength(50, ErrorMessage = "メールアドレスは50文字以内で入力してください。")]
    public string? EmpMailadress { get; set; } = string.Empty;

    /// <summary>
    /// 電話番号
    /// </summary>
    [Display(Name = "電話番号")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [RegularExpression(@"^0\d{1,4}-\d{1,4}-\d{4}$", ErrorMessage = "電話番号の形式（例: 03-1234-5678）で入力してください。")]
    [StringLength(20, ErrorMessage = "電話番号は20文字以内で入力してください。")]
    public string? EmpPhonenumber { get; set; } = string.Empty;

    /// <summary>
    /// 所属部門
    /// </summary>
    [Display(Name = "所属部門")]
    [Required(ErrorMessage = "{0}は選択必須です。")]
    public int? DeptId { get; set; } = 0;

    /// <summary>
    /// 選択された部門名
    /// </summary>
    [Display(Name = "部門名")]
    public string? DeptName { get; set; } = string.Empty;

    /// <summary>
    /// 部門のリストをSelectListItemのリストに変換してプロパティに設定する
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
    // 部門のリスト
    public List<SelectListItem>? Departments { get; set; } = null;

    public override string ToString()
    {
        return $"EmpName={EmpName} , EmpMailadress={EmpMailadress}, EmpPhonenumber={EmpPhonenumber}, DeptId={DeptId} , DeptName={DeptName} , Departments={Departments}";
    }
}