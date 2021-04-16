using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.Services
{
    public interface IUpdateRecipeMenuService
    {
        Task UpdateRecipeName();
        Task UpdateRecipeIngredient();
    }
}
