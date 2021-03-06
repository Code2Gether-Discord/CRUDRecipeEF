using CRUDRecipeEF.BL.DL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDRecipeEF.BL.DL.Data
{
    public class RecipeContext : DbContext
    {
        public RecipeContext()
        {
        }

        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}