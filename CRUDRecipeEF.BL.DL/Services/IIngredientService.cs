﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;

namespace CRUDRecipeEF.BL.DL.Services
{
    public interface IIngredientService
    {
        Task<IngredientDetailDTO> GetIngredientByName(string name);

        Task<IEnumerable<IngredientDetailDTO>> GetAllIngredients();

        Task<string> AddIngredient(IngredientAddDTO ingredient);

        Task DeleteIngredient(string name);
    }
}