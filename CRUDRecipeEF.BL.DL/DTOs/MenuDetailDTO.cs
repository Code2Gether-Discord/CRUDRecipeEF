using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.DTOs
{
    public class MenuDetailDTO
    {
        public string Name { get; set; }
        public List<RecipeDetailDTO> Recipes { get; set; } = new List<RecipeDetailDTO>();
    }
}
