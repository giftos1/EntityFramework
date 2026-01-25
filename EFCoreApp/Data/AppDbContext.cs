using EFCoreApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EFCoreApp.Data
{
//    NOTE You didn’t list Ingredient on AppDbContext, but EF Core models it correctly as it’s exposed on the Recipe.
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Global query filter to exclude soft-deleted recipes. Must manually set IsDeleted to true to soft delete.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasQueryFilter(r => !r.IsDeleted);
        }

        /* Override SaveChangesAsync to implement soft delete. No need to set up soft delete here if using global query filters.
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            foreach (var item in ChangeTracker.Entries<Blog>().Where(e => e.State == EntityState.Deleted))
            {
                item.State = EntityState.Modified;
                item.CurrentValues["IsDeleted"] = true;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }*/
        public DbSet<Recipe> Recipes { get; set; }
    }
}


/*EF Core has a handy feature called global query filters. These
filters allow you to specify a Where clause at the model level. You
could ensure, for example, that EF Core never loads Recipes for
which IsDeleted is true. This feature is also useful for
segregating data in a multitenant environment. See the
documentation for details: https://learn.microsoft.com/en-au/ef/core/querying/filters?tabs=ef10 */