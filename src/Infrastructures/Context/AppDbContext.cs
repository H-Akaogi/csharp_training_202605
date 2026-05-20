/// DbContext継承クラス
/// データベース接続およびセッション管理
/// エンティティの変更追跡（Change Tracking）
/// クエリ実行および結果のマッピング
/// SaveChangesによる永続化処理の統括

using Microsoft.EntityFrameworkCore;
using WebApp_Src.Infrastructures.Entities;
namespace WebApp_Src.Infrastructures.Context;
/// <summary>
/// DbContext継承クラス
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// employeeテーブルにアクセスするプロパティ
    /// </summary> 
    public DbSet<EmployeeEntity> Employees { get; set; } = null!;
    /// <summary>
    /// departmentテーブルにアクセスするプロパティ
    /// </summary> 
    public DbSet<DepartmentEntity> Departments { get; set; } = null!;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="options">
    ///  データベース接続情報 や ログ出力設定、トラッキング挙動の設定などのオプション
    /// </param>
    /// <returns></returns>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}