using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL
{
    public class Ingredient
    {
        public int Id { get; set; }

        public List<Recipe> Recipes = new List<Recipe>();
        public string Name { get; set; }

    }
}
