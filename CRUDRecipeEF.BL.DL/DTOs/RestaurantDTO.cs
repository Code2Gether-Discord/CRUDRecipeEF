using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDRecipeEF.BL.DL.DTOs
{
    public class RestaurantDTO
    {
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }
        public List<MenuDTO> Menus { get; set; } = new List<MenuDTO>();
    }
}
