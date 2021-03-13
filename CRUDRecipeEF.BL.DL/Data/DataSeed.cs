using System.Collections.Generic;
using CRUDRecipeEF.BL.DL.Entities;
using Microsoft.Extensions.Logging;

namespace CRUDRecipeEF.BL.DL.Data
{
    public class DataSeed : IDataSeed
    {
        private readonly ILogger<DataSeed> _logger;
        private readonly RecipeContext _context;

        public DataSeed(ILogger<DataSeed> logger,
            RecipeContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void Seed()
        {
            _logger.LogDebug("Seeding Database");

            _context.Recipes.AddRange(
                new List<Recipe>
                {
                    new Recipe { Id = 1, Name = "Chocolate Cake", Ingredients =  new List<Ingredient>
                    {
                        new Ingredient { Name = "Chocolate"},
                        new Ingredient { Name = "Flour"}
                    }, Category = new RecipeCategory { Name = "Cakes" } },
                    new Recipe { Id = 2, Name = "Apple pie", Ingredients =  new List<Ingredient>
                    {
                        new Ingredient { Name = "Apple"}
                    }},
                    new Recipe { Name = "Taco", Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "Meat"},
                        new Ingredient { Name = "Lettuce"}
                    }, Category = new RecipeCategory { Name = "Main dishes" }},
                });

            var chocCake = _context.Recipes.Find(1);
            var applePie = _context.Recipes.Find(2);
            var sugar = new Ingredient { Name = "Sugar" };

            chocCake.Ingredients.Add(sugar);
            applePie.Ingredients.Add(sugar);

            _context.SaveChanges();
        }

    }
}
