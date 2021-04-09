using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDRecipeEF.BL.DTOs
{
    public class RecipeAddDTO
    {
        public string Recipe { get; set; }
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        public List<IngredientDTO> Recipes { get; set; } = new();
    }
}