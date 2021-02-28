using System.Collections.Generic;

namespace CRUDRecipeEF.BL.DL.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }

        public List<Recipe> Recipes = new List<Recipe>();
        public string Name { get; set; }

    }
}
