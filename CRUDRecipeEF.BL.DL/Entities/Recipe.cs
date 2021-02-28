using System.Collections.Generic;

namespace CRUDRecipeEF.BL.DL.Entities
{
    public class Recipe
    {
        public int Id { get; set; }

        public List<Ingredient> Ingredients = new List<Ingredient>();
        public string Name { get; set; }

        
    }
}
