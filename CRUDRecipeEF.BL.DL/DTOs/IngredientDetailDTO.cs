using System.Collections.Generic;

namespace CRUDRecipeEF.BL.DL.DTOs
{
    public class IngredientDetailDTO
    {
        public string Name { get; set; }

        public List<RecipeDetailDTO> Recipes { get; set; } = new List<RecipeDetailDTO>();
    }
}
