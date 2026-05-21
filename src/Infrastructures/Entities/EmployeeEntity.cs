/// Entityクラス
/// ドメイン上のデータ構造を表すクラス
/// データベースのテーブルの1行に対応するオブジェクト
/// 状態（プロパティ）を保持する
/// 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApp_Src.Infrastructures.Entities;
/// <summary>
/// 従業員テーブル(employee)を扱うEntity Framework Coreのエンティティクラス
/// </summary>
[Table("employee")]
public class EmployeeEntity
{
    /// <summary>
    /// 従業員Id(主キー)
    /// </summary>
    [Key]
    [Column("id")]
    public int EmpId { get; set; }


    /// <summary>
    /// 従業員名
    /// </summary>
    [Column("name")]
    public string EmpName { get; set; } = string.Empty;

    /// メールアドレス
    [Column("email")]
    public string EmpMailadress { get; set; } = string.Empty;

    /// 電話番号
    [Column("phone")]
    public string EmpPhonenumber { get; set; } = string.Empty;


    /// <summary>
    /// 所属部署Id(外部キー)
    /// </summary>
    [Column("dept_id")]
    public int? DeptId { get; set; }
}