using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.Entities
{
    public class Menu
    {
        private string _name;

        public int Id { get; set; }

        [Required]
        public string Name { get => _name; set => _name = value.Trim(); }
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
