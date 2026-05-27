using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using WebApp_Src.Infrastructures.Context;
using WebApp_Src.Applications.Domains;
using WebApp_Src.Applications.Repositories;
using WebApp_Src.Infrastructures.Adapters;
using WebApp_Src.Exceptions;
using WebApp_Src.Infrastructures.Repositories;

namespace WebApp_src.Test.Infrastructures.Repositories;

[DoNotParallelize]
[TestClass]
public class DepartmentRepositoryTests
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=csharp_training_202605;Username=postgres;Password=training;";

    private DepartmentRepository _repository = null!;
    private AppDbContext _context = null!;

    [TestInitialize]
    // Arrange
    public void Setup()
    {
        var adapter = new DepartmentEntityAdapter();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        _context = new AppDbContext(options);

        var path = Path.Combine(AppContext.BaseDirectory, "sql", "init.sql");
        var sql = File.ReadAllText(path);
        _context.Database.ExecuteSqlRaw(sql);

        _repository = new DepartmentRepository(_context, adapter);
    }

    [TestMethod]
    public void GetAll_Result()
    {
        // Act
        var actual = _repository.GetAll();

        // Assert
        AreEqual(5, actual.Count);
        IsTrue(actual.Any(c => c.Equals(new Department(1, "総務部"))));
        IsTrue(actual.Any(c => c.Equals(new Department(2, "経理部"))));
        IsTrue(actual.Any(c => c.Equals(new Department(3, "人事部"))));
        IsTrue(actual.Any(c => c.Equals(new Department(4, "開発部"))));
        IsTrue(actual.Any(c => c.Equals(new Department(5, "営業部"))));
    }

    [TestMethod]
    public void FindById_WhenIdCorrect()
    {
        // Arrange
        var expected = new Department(1, "総務部");
        // Act
        var actual = _repository.FindById(1);
        // Assert
        AreEqual(expected, actual);
        AreEqual("総務部", actual?.Name);
    }

    [TestMethod]
    public void FindById_WhenIdNotFound()
    {
        // Act
        var actual = _repository.FindById(999);
        // Assert
        IsNull(actual);
    }

    [TestMethod]
    public void FindByName_WhenIdCorrect()
    {
        // Arrange
        var expected = new Department(1, "総務部");
        // Act
        var actual = _repository.FindByName("総務部");
        // Assert
        AreEqual(expected, actual);
        AreEqual(1, actual?.Id);
    }

    [TestMethod]
    public void FindByName_WhenIdNotFound()
    {
        // Act
        var actual = _repository.FindByName("こんにちは部");
        // Assert
        IsNull(actual);
    }

    [TestMethod]
    public void Create_WhenIdCorrect()
    {
        // Arrange
        var beforeCount = _context.Departments.Count();

        var expected = new Department(null, "製造部");

        // Act
        _repository.Create(expected);

        // Assert
        var afterCount = _context.Departments.Count();
        AreEqual(beforeCount + 1, afterCount);

        var created = _context.Departments;
        IsNotNull(created);
    }
    [TestMethod]
    public void Update_WhenIdCorrect()
    {
        // Arrange
        var beforeCount = _context.Departments.Count();

        var expected = new Department(2, "製造部");

        // Act
        _repository.Update(expected);

        // Assert
        var afterCount = _context.Departments.Count();
        AreEqual(beforeCount, afterCount);

        var updated = _context.Departments;
        IsNotNull(updated);
    }
}
