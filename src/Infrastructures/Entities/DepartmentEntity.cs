/// Entityクラス
/// ドメイン上のデータ構造を表すクラス
/// データベースのテーブルの1行に対応するオブジェクト
/// 状態（プロパティ）を保持する

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApp_Src.Infrastructures.Entities;
/// <summary>
/// 部門テーブル(department)を扱うEntity Framework Coreのエンティティクラス
/// </summary>
[Table("department")]
public class DepartmentEntity
{
    /// <summary>
    /// 部門Id(主キー)
    /// </summary> 
    [Key]
    [Column("id")]
    public int DeptId { get; set; }

    /// <summary>
    /// 部門名
    /// </summary> 
    [Column("name")]
    public string DeptName { get; set; } = string.Empty;
    public ICollection<EmployeeEntity> Employees { get; set; }
}