using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDRecipeEF.BL.DL.DTOs
{
    public class MenuDTO
    {
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }
        public List<RecipeDTO> Recipes { get; set; } = new List<RecipeDTO>();
    }
}
