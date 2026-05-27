using System.ComponentModel.DataAnnotations;
using System.Xml.XPath;
using WebApp_Src.Applications.Adapters;
using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Presentations.ViewModels;

public class DeleteDepartmentViewModel
{
    public int DeptId { get; set; }
    [Display(Name = "部門名")]

    public string? DeptName { get; set; } = string.Empty;
}