using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDRecipeEF.BL.DL.DTOs
{
    public class RecipeAddDTO
    {
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        public List<IngredientAddDTO> Ingredients { get; set; } = new List<IngredientAddDTO>();
    }
}
