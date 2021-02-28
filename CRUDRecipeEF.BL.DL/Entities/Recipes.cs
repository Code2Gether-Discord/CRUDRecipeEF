using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL
{
    public class Recipes
    {
        public int RecipeID { get; set; }

        public List<Ingridients> Ingridients { get; set; }
        public string RecipeName { get; set; }

        
    }
}
