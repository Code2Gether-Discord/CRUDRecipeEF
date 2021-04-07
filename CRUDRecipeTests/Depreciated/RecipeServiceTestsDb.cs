using System;
using System.Collections.Generic;
using CRUDRecipeEF.DAL.Data;
using CRUDRecipeEF.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDRecipeTests.Services
{
    public abstract class RecipeServiceTestsDb : IDisposable
    {
        protected RecipeServiceTestsDb(DbContextOptions<RecipeContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        protected DbContextOptions<RecipeContext> ContextOptions { get; }

        public void Dispose()
        {
            using var context = new RecipeContext(ContextOptions);
            context.Database.EnsureDeleted();
        }

        private void Seed()
        {
            using var context = new RecipeContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            List<Ingredient> fruitSaladIngredients = new()
            {
                new Ingredient
                {
                    Name = "Apple"
                },
                new Ingredient
                {
                    Name = "Orange"
                },
                new Ingredient
                {
                    Name = "Peach"
                }
            };

            var fruitSalad = new Recipe
            {
                Name = "Fruit Salad",
                Ingredients = fruitSaladIngredients
            };

            List<Ingredient> applePieIngredients = new()
            {
                new Ingredient
                {
                    Name = "Apple"
                },
                new Ingredient
                {
                    Name = "Crust"
                },
                new Ingredient
                {
                    Name = "Sugar"
                }
            };

            var applePie = new Recipe
            {
                Name = "Apple Pie",
                Ingredients = applePieIngredients
            };

            context.AddRange(fruitSalad, applePie);
            context.SaveChanges();
        }
    }
}