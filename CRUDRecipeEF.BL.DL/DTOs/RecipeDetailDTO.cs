using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.DTOs
{
    public class RecipeDetailDTO
    {
        public string Name { get; set; }
        public List<IngredientDetailDTO> Ingredients { get; set; } = new List<IngredientDetailDTO>();
    }
}
