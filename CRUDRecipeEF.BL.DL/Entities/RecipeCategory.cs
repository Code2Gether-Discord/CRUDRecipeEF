using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDRecipeEF.BL.DL.Entities
{
    public class RecipeCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}