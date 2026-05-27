/// DIコンテナ


using Microsoft.EntityFrameworkCore;
using WebApp_Src.Applications.Repositories;
using WebApp_Src.Applications.Services;
using WebApp_Src.Applications.Services.Impls;
using WebApp_Src.Infrastructures.Adapters;
using WebApp_Src.Infrastructures.Context;
using WebApp_Src.Infrastructures.Repositories;
using WebApp_Src.Presentations.Controllers;
using WebApp_Src.Presentations.ViewModels;
namespace WebApp_Src.Presentations.Extensions;
/// <summary>
/// 依存定義および依存性注入クラス
/// </summary>
public static class DependencyExtension
{
    /// <summary>
    /// アプリケーション全体の依存定義を設定する拡張メソッド
    /// </summary>
    /// <param name="services">DIコンテナ</param>
    /// <param name="configuration">アプリケーション環境</param>
    public static void SettingDependencyInjection(
        this IServiceCollection services, IConfiguration configuration)
    {
        // EntityFramework Coreのインスタンス生成と依存定義
        SettingEntityFrameworkCore(configuration, services);
        // インフラストラクチャ層のインスタンス生成と依存定義
        SettingInfrastructures(services);
        // アプリケーション層のインスタンス生成と依存定義
        SettingApplications(services);
        // プレゼンテーション層のインスタンス生成と依存定義
        SettingPresentations(services);
    }

    /// <summary>
    /// EntityFramework Coreのインスタンス生成と依存定義
    /// </summary>
    /// <param name="configuration">アプリケーション環境</param>
    /// <param name="services">DIコンテナ</param>
    private static void SettingEntityFrameworkCore(IConfiguration configuration, IServiceCollection services)
    {
        // 接続文字列(appsettings.json)から取得
        var connectionString = configuration.GetConnectionString("PostgreSqlConnection");
        // DbContext登録(PostgreSQL用)
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

    /// <summary>
    /// インフラストラクチャ層のインスタンス生成と依存定義
    /// </summary>
    /// <param name="services">DIコンテナ</param>
    private static void SettingInfrastructures(IServiceCollection services)
    {
        // ドメインモデル:部署と部署エンティティの相互変換インターフェイスの実装
        services.AddScoped<DepartmentEntityAdapter>();
        // ドメインモデル:社員と社員エンティティの相互変換インターフェイスの実装
        services.AddScoped<EmployeeEntityAdapter>();
        // ドメインオブジェクト:部署のCRUD操作インターフェイス実装
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        // ドメインオブジェクト:社員のCRUD操作インターフェイスの実装
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    }

    /// <summary>
    /// アプリケーション層のインスタンス生成と依存定義
    /// </summary>
    /// <param name="services">DIコンテナ</param>
    private static void SettingApplications(IServiceCollection services)
    {
        // 社員登録サービスインターフェイスの実装
        services.AddScoped<IEmployeeRegisterService, EmployeeRegisterService>();
        // 部門登録サービスインターフェイスの実装
        services.AddScoped<IDepartmentRegisterService, DepartmentRegisterService>();
    }

    /// <summary>
    /// プレゼンテーション層のインスタンス生成と依存定義
    /// </summary>
    /// <param name="services">DIコンテナ</param>
    private static void SettingPresentations(IServiceCollection services)
    {
        // 社員登録ViewModelをドメインオブジェクト:社員に変換するアダプターインターフェイスの実装
        services.AddScoped<EmployeeRegisterViewModelAdapter>();
        // TempDataへのEmployeeRegisterViewの保存・復元するためのクラス
        // コンストラクタを利用して明示的にDIコンテナにインスタンスを登録する
        services.AddScoped(
            provider =>
            new TempDataStore<EmployeeRegisterViewModel>("EmployeeRegisterViewModel")
        );


        // 部門一覧ViewModelをドメインオブジェクト:部門に変換するアダプターインターフェイスの実装
        services.AddScoped<DepartmentRegisterViewModelAdapter>();
        // TempDataへのDepartmentListViewの保存・復元するためのクラス
        // コンストラクタを利用して明示的にDIコンテナにインスタンスを登録する
        services.AddScoped(
            provider =>
            new TempDataStore<DepartmentRegisterViewModel>("DepartmentRegisterViewModel")
        );


        // 部門一覧ViewModelをドメインオブジェクト:部門に変換するアダプターインターフェイスの実装
        services.AddScoped<DepartmentListViewModelAdapter>();
        // TempDataへのDepartmentListViewの保存・復元するためのクラス
        // コンストラクタを利用して明示的にDIコンテナにインスタンスを登録する
        services.AddScoped(
            provider =>
            new TempDataStore<DepartmentListViewModel>("DepartmentListViewModel")
        );
        // 部門一覧Serviceをドメインオブジェクト:部門に変換するアダプターインターフェイスの実装
        services.AddScoped<IDepartmentListService, DepartmentListService>();


        services.AddScoped<EmployeeListViewModelAdapter>();
        services.AddScoped(
            provider =>
            new TempDataStore<EmployeeListViewModel>("EmployeeListViewModel")
        );
        services.AddScoped<IEmployeeListService, EmployeeListService>();

        services.AddScoped<DeleteEmployeeViewModelAdapter>();
        services.AddScoped(
            provider =>
            new TempDataStore<DeleteEmployeeViewModel>("DeleteEmployeeViewModel")
        );
        services.AddScoped<IDeleteEmployeeService, DeleteEmployeeService>();

        services.AddScoped<EmployeeUpdateViewModelAdapter>();
        services.AddScoped(
            provider =>
            new TempDataStore<EmployeeUpdateViewModel>("EmployeeUpdateViewModel")
        );
        services.AddScoped<IEmployeeUpdateService, EmployeeUpdateService>();

        services.AddScoped<DepartmentUpdateViewModelAdapter>();
        services.AddScoped(
            provider =>
            new TempDataStore<DepartmentUpdateViewModel>("DepartmentUpdateViewModel")
        );
        services.AddScoped<IDepartmentUpdateService, DepartmentUpdateService>();
    }
}