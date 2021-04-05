using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDRecipeEF.BL.DTOs
{
    public class RecipeCategoryDTO
    {
        public int Id { get; set; }

        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        public List<RecipeDTO> Recipes { get; set; } = new List<RecipeDTO>();
    }
}