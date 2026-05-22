using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp_Exercise.Applications.Domains;
using WebApp_Exercise.Exceptions;
using WebApp_Exercise.Infrastructures.Adapters;
using WebApp_Exercise.Infrastructures.Context;
using WebApp_Exercise.Infrastructures.Entities;
using WebApp_Exercise.Infrastructures.Repositories;
using WebApp_Exercise.Tests.TestDoubles;

namespace WebApp_Exercise.Tests.Infrastructures.Repositories;

[TestClass]
public sealed class ItemCategoryRepositoryTests
{
    [TestMethod]
    public void FindAll_ReturnsAllCategories()
    {
        using var context = CreateContext(
        [
            new ItemCategoryEntity { Id = 1, Name = "Books" },
            new ItemCategoryEntity { Id = 2, Name = "Games" },
        ]);
        var repository = CreateRepository(context);

        var categories = repository.FindAll();

        Assert.AreEqual(2, categories.Count);
        AssertCategory(categories[0], 1, "Books");
        AssertCategory(categories[1], 2, "Games");
    }

    [TestMethod]
    public void FindById_WhenCategoryExists_ReturnsCategory()
    {
        using var context = CreateContext(
        [
            new ItemCategoryEntity { Id = 1, Name = "Books" },
            new ItemCategoryEntity { Id = 2, Name = "Games" },
        ]);
        var repository = CreateRepository(context);

        var category = repository.FindById(2);

        Assert.IsNotNull(category);
        AssertCategory(category, 2, "Games");
    }

    [TestMethod]
    public void FindById_WhenCategoryDoesNotExist_ReturnsNull()
    {
        using var context = CreateContext(
        [
            new ItemCategoryEntity { Id = 1, Name = "Books" },
        ]);
        var repository = CreateRepository(context);

        var category = repository.FindById(2);

        Assert.IsNull(category);
    }

    [TestMethod]
    public void FindAll_WhenDbSetThrows_WrapsExceptionInInternalException()
    {
        using var context = CreateContext(new ThrowingDbSet<ItemCategoryEntity>());
        var repository = CreateRepository(context);

        var exception = Assert.ThrowsException<InternalException>(() => repository.FindAll());

        Assert.IsInstanceOfType<InvalidOperationException>(exception.InnerException);
    }

    private static ItemCategoryRepository CreateRepository(AppDbContext context)
    {
        return new ItemCategoryRepository(context, new ItemCategoryEntityAdapter());
    }

    private static AppDbContext CreateContext(IEnumerable<ItemCategoryEntity> entities)
    {
        return CreateContext(new QueryableDbSet<ItemCategoryEntity>(entities));
    }

    private static AppDbContext CreateContext(DbSet<ItemCategoryEntity> itemCategories)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().Options;
        return new AppDbContext(options)
        {
            ItemCategories = itemCategories,
        };
    }

    private static void AssertCategory(ItemCategory category, int id, string name)
    {
        Assert.AreEqual(id, category.Id);
        Assert.AreEqual(name, category.Name);
    }
}
