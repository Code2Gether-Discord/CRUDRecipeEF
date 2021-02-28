using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL
{
    public class Ingridients
    {
        public int IngridientID { get; set; }

        public List<Recipes> Recipes { get; set; }
        public string IngridientName { get; set; }

    }
}
