using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDRecipeEF.BL.DL.Entities
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}