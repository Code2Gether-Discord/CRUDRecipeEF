using System;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDRecipeTests.Services
{
    public abstract class IngredientServiceTestsDb : IDisposable
    {
        protected DbContextOptions<RecipeContext> ContextOptions { get; }

        protected IngredientServiceTestsDb(DbContextOptions<RecipeContext> contextOptions)
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

        public void Dispose()
        {
            using var context = new RecipeContext(ContextOptions);
            context.Database.EnsureDeleted();
        }
    }
}
