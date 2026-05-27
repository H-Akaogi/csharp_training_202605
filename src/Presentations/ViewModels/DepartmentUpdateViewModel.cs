using System.ComponentModel.DataAnnotations;
using System.Xml.XPath;
using WebApp_Src.Applications.Adapters;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;

public class DepartmentUpdateViewModel
{
    public int DeptId { get; set; }
    [Display(Name = "部門名")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [StringLength(20, ErrorMessage = "部門名は20文字以内で入力してください。")]

    public string? DeptName { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"DeptName={DeptName}";
    }
}