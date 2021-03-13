using AutoMapper;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;
using CRUDRecipeEF.BL.DL.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CRUDRecipeTests
{
    public class SQLiteIngredientServiceTests : IngredientServiceTests
    {
        private MapperConfiguration autoMapperConfig;

        public SQLiteIngredientServiceTests() : 
            base(new DbContextOptionsBuilder<RecipeContext>().UseSqlite("Filename=Test.db")
                .Options)
        {
            autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Ingredient, IngredientDetailDTO>());
        }

        [Fact]
        public async Task Test_GetIngredientByName()
        {
            using (var context = new RecipeContext(ContextOptions))
            {
                var ingredientService = new IngredientService(context, new Mapper(autoMapperConfig));

                var test = await ingredientService.GetAllIngredients();
                var ing = await ingredientService.GetIngredientByName("Apple");
                Assert.NotNull(ing);
            }
        }
    }
}
