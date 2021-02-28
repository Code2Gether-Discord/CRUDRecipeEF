using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL
{
    public class Recipe
    {
        public int Id { get; set; }

        public List<Ingredient> Ingredients = new List<Ingredient>();
        public string Name { get; set; }

        
    }
}
