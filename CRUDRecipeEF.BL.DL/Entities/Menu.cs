using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
