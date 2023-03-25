using Application.Interfaces.Contexts;
using Domain.Attributes;
using Domain.Categories;
using Domain.Users;
using EndPoint.Shared.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfigurations;
using Persistence.Seeds;

namespace Persistence.Contexts;

public class DataBaseContext : IdentityDbContext<User> , IDataBaseContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
        
    }

    public new DbSet<User> Users { get; set; } = null!;
    public  DbSet<CategoryType> CategoryTypes { get; set; } = null!;
    public  DbSet<CategoryBrand> CategoryBrands { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        IdentityConfigure(modelBuilder);
        ApplyConfiguration(modelBuilder);
        DataBaseContextSeed.CategorySeed(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>(ShadowProperty.InsertTime).HasDefaultValue(DateTime.Now);
                modelBuilder.Entity(entityType.Name).Property<DateTime?>(ShadowProperty.UpdateTime);
                modelBuilder.Entity(entityType.Name).Property<DateTime?>(ShadowProperty.RemoveTime);
                modelBuilder.Entity(entityType.Name).Property<bool>(ShadowProperty.IsRemoved).HasDefaultValue(false);
            }
        }

        
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var modifiedEntries = ChangeTracker.Entries()
            .Where(x => x.State is EntityState.Added or EntityState.Deleted or EntityState.Modified);

        foreach (var item in modifiedEntries)
        {
            var entityType = item.Context.Model.FindEntityType(item.Entity.GetType())!;

            var insertTime = entityType.FindProperty(ShadowProperty.InsertTime);
            var updateTime = entityType.FindProperty(ShadowProperty.UpdateTime);
            var removeTime = entityType.FindProperty(ShadowProperty.RemoveTime);
            var isRemoved = entityType.FindProperty(ShadowProperty.IsRemoved);

            if (item.State == EntityState.Added && insertTime != null)
            {
                item.Property(ShadowProperty.InsertTime).CurrentValue = DateTime.Now;
            }

            if (item.State == EntityState.Modified && insertTime != null)
            {
                item.Property(ShadowProperty.UpdateTime).CurrentValue = DateTime.Now;
            }
            if (item.State == EntityState.Deleted && insertTime != null)
            {
                item.Property(ShadowProperty.UpdateTime).CurrentValue = DateTime.Now;
                item.Property(ShadowProperty.IsRemoved).CurrentValue = true;
                item.State = EntityState.Modified;
            }

        }


        return base.SaveChangesAsync(cancellationToken);
    }

    private static void ApplyConfiguration(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CategoryBrandEntityTypeConfiguration());
        builder.ApplyConfiguration(new CategoryTypeEntityTypeConfiguration());
    }

    private static void IdentityConfigure(ModelBuilder builder)
    {
        builder.Entity<IdentityUser<string>>().ToTable("Users","identity");
        builder.Entity<IdentityRole<string>>().ToTable("Roles","identity");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims","identity");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims","identity");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins","identity");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles","identity");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens","identity");

        builder.Entity<IdentityUserLogin<string>>().HasKey(p => new {p.LoginProvider,p.ProviderKey});
        builder.Entity<IdentityUserRole<string>>().HasKey(p => new {p.UserId,p.RoleId});
        builder.Entity<IdentityUserToken<string>>().HasKey(p => new {p.LoginProvider,p.UserId,p.Name});

    }
}