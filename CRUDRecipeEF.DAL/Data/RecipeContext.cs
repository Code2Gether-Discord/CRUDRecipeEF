using CRUDRecipeEF.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDRecipeEF.DAL.Data
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {
        }

        public DbSet<RecipeCategory> RecipeCategories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}