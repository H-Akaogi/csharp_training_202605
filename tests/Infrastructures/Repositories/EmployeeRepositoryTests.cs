
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using WebApp_Src.Infrastructures.Context;
using WebApp_Src.Applications.Domains;
using WebApp_Src.Applications.Repositories;
using WebApp_Src.Infrastructures.Adapters;
using WebApp_Src.Exceptions;
using WebApp_Src.Infrastructures.Repositories;
using Microsoft.VisualBasic;
using System.Data.Common;
namespace WebApp_Src.Test.Infrastructures.Repositories;

[DoNotParallelize]
[TestClass]
public class EmployeeRepositoryTests
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=csharp_training_202605;Username=postgres;Password=training;";

    private EmployeeRepository _repository = null!;
    private AppDbContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var employeeAdapter = new EmployeeEntityAdapter();
        var departmentAdapter = new DepartmentEntityAdapter();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        _context = new AppDbContext(options);

        var path = Path.Combine(AppContext.BaseDirectory, "sql", "init.sql");
        var sql = File.ReadAllText(path);
        _context.Database.ExecuteSqlRaw(sql);

        _repository = new EmployeeRepository(_context, employeeAdapter, departmentAdapter);

    }

    [TestMethod]
    public void Create_WhenCorrect()
    {
        // Arrange
        var beforeCount = _context.Employees.Count();

        var department = new Department(2, "経理部");
        var employee = new Employee(null, "伊藤 万紀子", "makiko_ito@example.co.jp", "090-0672-5822", new Department(2, "経理部"));
        employee.ChangeDepartment(department);
        // Act
        _repository.Create(employee);
        // Assert
        var afterCount = _context.Employees.Count();
        AreEqual(beforeCount + 1, afterCount);

        var created = _context.Employees
            .Include(i => i.Department)
            .FirstOrDefault(i => i.EmpName == "伊藤 万紀子");

        IsNotNull(created);
    }

    /// <summary>
    ///  社員削除
    /// </summary>
    [TestMethod] // 正常系
    public void DeleteById_WhenIdCorrect()
    {
        // Arrange
        var beforeCount = _context.Employees.Count();

        // Act
        var actual = _repository.DeleteById(1);

        // Assert
        var afterCount = _context.Employees.Count();
        AreEqual(beforeCount - 1, afterCount);

        var deleted = _context.Employees
            .Include(i => i.Department)
            .FirstOrDefault(i => i.EmpName == "田中太郎");

        IsNull(deleted);
    }

    [TestMethod] // 異常系
    public void DeleteById_WhenIdNotFound()
    {
        // Act
        // Assert
        var e = Assert.ThrowsException<InternalException>(() =>
        _repository.DeleteById(999));

        Assert.AreEqual("社員の削除ができませんでした。", e.Message);
    }

    /// <summary>
    /// 社員Idから社員名を探す
    /// </summary>
    [TestMethod] // 正常系
    public void FindById_WhenIdCorrect()
    {
        // Act
        var actual = _repository.FindById(3);
        // Assert
        IsNotNull(actual);
        AreEqual(3, actual.EmpId);
        AreEqual("佐藤花子", actual.EmpName);
        AreEqual("sastouhanako@example.com", actual.EmpMailadress);
        AreEqual("090-0000-0003", actual.EmpPhonenumber);
        AreEqual(4, actual.Department?.Id);
    }

    [TestMethod] // 異常系
    public void FindById_WhenIdNotFound()
    {
        // Act
        var actual = _repository.FindById(999);
        // Assert
        IsNull(actual);
    }

    /// <summary>
    /// メールアドレスから社員名を探す
    /// </summary>
    [TestMethod] // 正常系
    public void FindByMail_WhenIdCorrect()
    {
        // Act
        var actual = _repository.FindByMail("sastouhanako@example.com");
        // Assert
        IsNotNull(actual);
        AreEqual("sastouhanako@example.com", actual.EmpMailadress);
    }

    [TestMethod] // 異常系
    public void FindByMail_WhenIdNotFound()
    {
        // Act
        var actual = _repository.FindByMail("thisisjustsumple@example.com");
        // Assert
        IsNull(actual);
    }

    /// <summary>
    /// 電話番号から社員名を探す
    /// </summary>
    [TestMethod] // 正常系
    public void FindByPhone_WhenIdCorrect()
    {
        // Act
        var actual = _repository.FindByPhone("090-0000-0003");
        // Assert
        IsNotNull(actual);
        AreEqual("090-0000-0003", actual.EmpPhonenumber);
    }

    [TestMethod] // 異常系
    public void FindByPhone_WhenIdNotFound()
    {
        // Act
        var actual = _repository.FindByPhone("9999-9999-9999");
        // Assert
        IsNull(actual);
    }

    [TestMethod]
    public void GetAll_Result()
    {
        // Act
        var actual = _repository.GetAll();

        // Assert
        AreEqual(8, actual.Count);
        IsTrue(actual.Any(c => c.Equals(new Employee(1, "田中太郎", "tanakatarou@example.com", "090-0000-0001", new Department(2, "経理部")))));
        IsTrue(actual.Any(c => c.Equals(new Employee(2, "鈴木三郎", "suzukisaburou@example.com", "090-0000-0002", new Department(1, "総務部")))));
        IsTrue(actual.Any(c => c.Equals(new Employee(3, "佐藤花子", "sastouhanako@example.com", "090-0000-0003", new Department(4, "開発部")))));
        IsTrue(actual.Any(c => c.Equals(new Employee(4, "中田彩子", "nakataayako@example.com", "090-0000-0004", new Department(5, "営業部")))));
        IsTrue(actual.Any(c => c.Equals(new Employee(5, "加藤圭太", "katoukeita@example.com", "090-0000-0005", new Department(3, "人事部")))));
        IsTrue(actual.Any(c => c.Equals(new Employee(6, "松本良太", "matumotoryouta@example.com", "090-0000-0006", new Department(4, "開発部")))));
        IsTrue(actual.Any(c => c.Equals(new Employee(7, "山下孝輔", "yamasitakousuke@example.com", "090-0000-0007", new Department(5, "営業部")))));
        IsTrue(actual.Any(c => c.Equals(new Employee(8, "渡辺大輔", "watanabedaisuke@example.com", "090-0000-0008", new Department(4, "開発部")))));
    }
    [TestMethod] // 正常系
    public void Update_WhenIdCorrect()
    {
        // Arrange
        var beforeCount = _context.Employees.Count();

        // Act
        var employee = new Employee(2, "伊藤 万紀子", "makiko_ito@example.co.jp", "090-0672-5822", new Department(2, "経理部"));
        _repository.Update(employee);
        // Assert
        var afterCount = _context.Employees.Count();
        AreEqual(beforeCount, afterCount);
        IsNotNull(employee);
    }
}
