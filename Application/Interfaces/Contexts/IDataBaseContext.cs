using Domain.Categories;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Contexts;

public interface IDataBaseContext
{
    DbSet<User> Users { get; set; }
    DbSet<CategoryType> CategoryTypes { get; set; }
    DbSet<CategoryBrand> CategoryBrands { get; set; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    
}