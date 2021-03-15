using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.DTOs
{
    public class RestaurantDetailDTO
    {
        public string Name { get; set; }
        public List<RestaurantDetailDTO> Menus { get; set; } = new List<RestaurantDetailDTO>();
    }
}
