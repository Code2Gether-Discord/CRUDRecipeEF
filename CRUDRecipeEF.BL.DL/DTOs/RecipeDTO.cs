using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDRecipeEF.BL.DL.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }

        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        public List<IngredientDTO> Ingredients { get; set; } = new List<IngredientDTO>();
    }
}