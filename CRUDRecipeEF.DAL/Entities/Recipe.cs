using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDRecipeEF.DAL.Entities
{
    public class Recipe
    {
        private string _name;

        public int Id { get; set; }

        [Required]
        public string Name { get => _name; set => _name = value.Trim(); }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        //for example salad, cakes
        public RecipeCategory Category { get; set; }
    }
}