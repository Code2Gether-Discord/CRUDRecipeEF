using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.DTOs
{
    public class MenuAddDTO
    {
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }
        public List<RecipeAddDTO> Recipes { get; set; } = new List<RecipeAddDTO>();
    }
}
