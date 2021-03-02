﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDRecipeEF.BL.DL.DTOs
{
    public class IngredientAddDTO
    {
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        public List<RecipeAddDTO> Recipes { get; set; } = new List<RecipeAddDTO>();
    }
}
