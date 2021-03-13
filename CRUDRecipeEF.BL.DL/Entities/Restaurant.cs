using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        [Required]
        public string RestauarantName { get; set; }
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
