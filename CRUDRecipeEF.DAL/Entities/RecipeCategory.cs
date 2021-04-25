using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDRecipeEF.DAL.Entities
{
    public class RecipeCategory
    {
        private string _name;

        public int Id { get; set; }

        [Required]
        public string Name { get => _name; set => _name = value.Trim().ToLower(); }
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}