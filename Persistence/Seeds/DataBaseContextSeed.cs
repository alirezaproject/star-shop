using Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Seeds;

public class DataBaseContextSeed
{
    public static void CategorySeed(ModelBuilder modelBuilder)
    {
        foreach (var categoryType in GetCategoryTypes())
        {
            modelBuilder.Entity<CategoryType>().HasData(categoryType);
        }
        foreach (var categoryBrand in GetCategoryBrands())
        {
            modelBuilder.Entity<CategoryBrand>().HasData(categoryBrand);
        }
    }


    private static IEnumerable<CategoryType> GetCategoryTypes()
    {
        return new List<CategoryType>()
        {
            new() { Id = 1, Type = "کالای دیجیتال" },
            new() { Id = 2, Type = "لوازم جانبی گوشی", ParentCategoryTypeId = 1 },
            new() { Id = 3, Type = "پایه نگهدارنده گوشی", ParentCategoryTypeId = 2 },
            new() { Id = 4, Type = "پاور بانک", ParentCategoryTypeId = 2 },
            new() { Id = 5, Type = "کیف و کاور گوشی", ParentCategoryTypeId = 2 },
        };
    }

    private static IEnumerable<CategoryBrand> GetCategoryBrands()
    {
        return new List<CategoryBrand>()
        {
            new() { Id = 1, Brand = "سامسونگ" },
            new() { Id = 2, Brand = "شیائومی" },
            new() { Id = 3, Brand = "اپل" },
            new() { Id = 4, Brand = "هوآوی" },
            new() { Id = 5, Brand = "نوکیا" },
            new() { Id = 6, Brand = "ال جی" },
      
        };

    }
}