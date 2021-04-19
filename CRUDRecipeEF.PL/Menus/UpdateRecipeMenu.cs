using CRUDRecipeEF.BL.Services;
using CRUDRecipeEF.DAL.Data;
using CRUDRecipeEF.DAL.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.PL.Menus
{
    public class UpdateRecipeMenu : IUpdateRecipeMenu
    {
        private readonly IRecipeService _recipeService;
        private readonly IIngredientService _ingredientService;
        private readonly IUpdateRecipeMenuService _updateRecipeMenuService;
        private readonly RecipeContext _context;
        private readonly ILogger _logger;

        private enum RecipeUpdateOption
        {
            InValid = 0, Name = 1, Ingredient = 2, GoBack = 3
        };

        public UpdateRecipeMenu(IRecipeService recipeService,
            IIngredientService ingredientService,
            RecipeContext context,
            ILogger<RecipeMenu> logger,
            IUpdateRecipeMenuService updateRecipeMenuService)
        {
            _recipeService = recipeService;
            _ingredientService = ingredientService;
            _logger = logger;
            _context = context;
            _updateRecipeMenuService = updateRecipeMenuService;
        }
        RecipeDTO findRecipe = new RecipeDTO();

        public async Task Show()
        {
            ConsoleHelper.ColorWriteLine("Please provide the name of the recipe that you want to update: ");
            var recipe = Console.ReadLine();

            var findRecipe = await _context.Recipes.Include(i => i.Ingredients)
                .SingleOrDefaultAsync(r => r.Name.ToLower() == recipe.ToLower().Trim());

            if (findRecipe != null)
            {
                Console.WriteLine();
                ConsoleHelper.DefaultColor = ConsoleColor.Blue;
                ConsoleHelper.ColorWriteLine(ConsoleColor.Yellow, "Recipe Change Menu");
                Console.WriteLine();
                ConsoleHelper.ColorWriteLine("1.) Change Name");
                ConsoleHelper.ColorWriteLine("2.) Change Ingredient");
                Console.WriteLine();
                ConsoleHelper.ColorWriteLine(ConsoleColor.Red, "3.) Back to Main Menu");
                Console.WriteLine();

                string input = string.Empty;
                int option = 0;
                bool valid = false;

                while (!valid)
                {
                    ConsoleHelper.ColorWrite(ConsoleColor.Yellow, "Please select an option: ");
                    input = Console.ReadLine();

                    valid = ConsoleHelper.ValidateInt(input, (int)RecipeUpdateOption.Name, (int)RecipeUpdateOption.GoBack, out option);

                    if (!Enum.IsDefined(typeof(RecipeUpdateOption), option))
                    {
                        _logger.LogWarning("Option is not in enum");
                        valid = false;
                    }

                }

                RecipeUpdateOption choice = (RecipeUpdateOption)option;
                await ExecuteUpdateRecipeSelection(choice);
            }
            else
            {
                Console.WriteLine();
                ConsoleHelper.ColorWriteLine(ConsoleColor.DarkYellow, $"{recipe} does not exist.");
                Console.WriteLine();
            }
        }

        private async Task ExecuteUpdateRecipeSelection(RecipeUpdateOption recipeUpdateOption)
        {
            switch (recipeUpdateOption)
            {
                case RecipeUpdateOption.InValid:
                    _logger.LogWarning("Attempted to execute invalid menu selection");
                    break;
                case RecipeUpdateOption.Name:
                    Console.WriteLine();
                    await UpdateRecipeName();
                    break;
                case RecipeUpdateOption.Ingredient:
                    Console.WriteLine();
                    await UpdateRecipeIngredient();
                    break;
                case RecipeUpdateOption.GoBack:
                    Console.WriteLine();
                    break;
                default:
                    break;
            }
        }

        private async Task UpdateRecipeIngredient()
        {
            ConsoleHelper.ColorWriteLine("Which one is the ingredient you want to change: ");
            var input = Console.ReadLine();

            IngredientDTO ingredient = new IngredientDTO();

            ingredient = await _ingredientService.GetIngredientDTOByNameAsync(input);

            ConsoleHelper.ColorWriteLine("Which is the new name of the ingredient: ");
            var newName = Console.ReadLine();

            await _updateRecipeMenuService.UpdateIngredient(ingredient, newName);
        }

        private async Task UpdateRecipeName()
        {
            ConsoleHelper.ColorWriteLine("Which is the new recipe name: ");
            var newName = Console.ReadLine();

            await _updateRecipeMenuService.UpdateRecipeName(findRecipe, newName);
        }
        
    }
}
