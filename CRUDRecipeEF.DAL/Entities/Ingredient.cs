using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDRecipeEF.DAL.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}