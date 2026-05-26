
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
public class ItemRepositoryTests
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=cs_db_exercise;Username=postgres;Password=training;";

    private ItemRepository _repository = null!;
    private AppDbContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var itemAdapter = new ItemEntityAdapter();
        var stockAdapter = new ItemStockEntityAdapter();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        _context = new AppDbContext(options);

        var path = Path.Combine(AppContext.BaseDirectory, "sql", "init.sql");
        var sql = File.ReadAllText(path);
        _context.Database.ExecuteSqlRaw(sql);

        _repository = new ItemRepository(_context, itemAdapter, stockAdapter);
    }

    [TestMethod]
    public void ExistsByName_WhenNameExists()
    {
        var actual = _repository.ExistsByName("水性ボールペン(黒)");
        IsTrue(actual);
    }

    [TestMethod]
    public void ExistsByName_WhenNameNotExists()
    {
        var actual = _repository.ExistsByName("存在しない商品名");
        IsFalse(actual);
    }

    [TestMethod]
    public void FindById_WhenIdCorrect()
    {
        var actual = _repository.FindById(1);

        IsNotNull(actual);
        AreEqual(1, actual.Id);
        AreEqual("水性ボールペン(黒)", actual.Name);
        AreEqual(120, actual.Price);
        IsNotNull(actual.ItemStock);
        AreEqual(20, actual.ItemStock.Stock);
    }

    [TestMethod]
    public void FindById_WhenIdNotFound()
    {
        var actual = _repository.FindById(999);
        IsNull(actual);
    }

    [TestMethod]
    public void Create_WhenCorrect()
    {
        var beforeCount = _context.Items.Count();

        var category = new ItemCategory(2, "雑貨");
        var item = new Item("検証用商品", 1500);
        item.ChangeItemCategory(category);
        item.ChangeStock(new ItemStock(8));

        _repository.Create(item);

        var afterCount = _context.Items.Count();
        AreEqual(beforeCount + 1, afterCount);

        var created = _context.Items
            .Include(i => i.Stock)
            .FirstOrDefault(i => i.Name == "検証用商品");

        IsNotNull(created);
        IsNotNull(created.Stock);
        AreEqual(8, created.Stock.Stock);
    }
}
