using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using WebApp_Exercise.Applications.Domains;
using WebApp_Exercise.Infrastructures.Adapters;
using WebApp_Exercise.Infrastructures.Context;
using WebApp_Exercise.Infrastructures.Repositories;

namespace WebApp_Exercise.Test.Infrastructures.Repositories;

[DoNotParallelize]
[TestClass]
public class ItemCategoryRepositoryTests
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=cs_db_exercise;Username=postgres;Password=training;";

    private ItemCategoryRepository _repository = null!;
    private AppDbContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var adapter = new ItemCategoryEntityAdapter();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        _context = new AppDbContext(options);

        var path = Path.Combine(AppContext.BaseDirectory, "sql", "init.sql");
        var sql = File.ReadAllText(path);
        _context.Database.ExecuteSqlRaw(sql);

        _repository = new ItemCategoryRepository(_context, adapter);
    }

    [TestMethod]
    public void FindAll_Result()
    {
        var actual = _repository.FindAll();

        AreEqual(3, actual.Count);
        IsTrue(actual.Any(c => c.Equals(new ItemCategory(1, "文房具"))));
        IsTrue(actual.Any(c => c.Equals(new ItemCategory(2, "雑貨"))));
        IsTrue(actual.Any(c => c.Equals(new ItemCategory(3, "パソコン周辺機器"))));
    }

    [TestMethod]
    public void FindById_WhenIdCorrect()
    {
        var expected = new ItemCategory(1, "文房具");
        var actual = _repository.FindById(1);

        AreEqual(expected, actual);
        AreEqual("文房具", actual?.Name);
    }

    [TestMethod]
    public void FindById_WhenIdNotFound()
    {
        var actual = _repository.FindById(999);
        IsNull(actual);
    }
}
