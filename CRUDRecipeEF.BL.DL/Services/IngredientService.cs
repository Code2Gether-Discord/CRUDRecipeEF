using CRUDRecipeEF.BL.DL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.Services
{
    class IngredientService : IIngredientService
    {
        public Task<string> AddIngredient(IngredientAddDTO ingredient)
        {
            throw new NotImplementedException();
        }

        public Task DeleteIngredient(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IngredientDetailDTO>> GetAllIngredients()
        {
            throw new NotImplementedException();
        }

        public Task<IngredientDetailDTO> GetIngredientByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
