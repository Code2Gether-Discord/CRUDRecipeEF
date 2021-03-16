using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDRecipeTests.Services
{
    public abstract class IngredientServiceTests
    {
        protected DbContextOptions<RecipeContext> ContextOptions { get; }

        protected IngredientServiceTests(DbContextOptions<RecipeContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        private void Seed()
        {
            using var context = new RecipeContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var apple = new Ingredient { Name = "Apple" };
            var orange = new Ingredient { Name = "Orange" };
            var peach = new Ingredient { Name = "Peach" };

            context.AddRange(apple, orange, peach);
            context.SaveChanges();
        }
    }
}
