using System;
using System.Collections.Generic;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Services;
using static System.Console;

namespace CRUDRecipeEF.PL.Menus
{
    public class MainMenu
    {
        private readonly IRecipeService _recipeService;

        public MainMenu(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public void Run()
        {
            WriteLine("Hello there!");
            _recipeService.AddRecipe(new RecipeAddDTO() { Name = "brains" });
            WriteLine(_recipeService.GetRecipeByName("human brains"));
        }
    }
}