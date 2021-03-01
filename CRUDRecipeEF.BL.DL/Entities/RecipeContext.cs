using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.Entities
{
    public class RecipeContext : DbContext
    {

        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {
            
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

    }
}
