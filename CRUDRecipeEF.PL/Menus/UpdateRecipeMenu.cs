using CRUDRecipeEF.BL.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.PL.Menus
{
    public class UpdateRecipeMenu : IUpdateMenu
    {
        private readonly IRecipeService _recipeService;
        private readonly IIngredientService _ingredientService;
        private readonly IUpdateRecipeMenuService _updateMenuService;
        private readonly ILogger _logger;

        private enum RecipeUpdateOption
        {
            InValid = 0, Name = 1, Ingredient = 2, GoBack = 3
        };

        public UpdateRecipeMenu(IRecipeService recipeService,
            IIngredientService ingredientService,
            ILogger<RecipeMenu> logger)
        {
            _recipeService = recipeService;
            _ingredientService = ingredientService;
            _logger = logger;
        }

        public async Task Show()
        {
            ConsoleHelper.DefaultColor = ConsoleColor.Blue;
            ConsoleHelper.ColorWriteLine(ConsoleColor.Yellow, "Recipe Change");
            Console.WriteLine();
            ConsoleHelper.ColorWriteLine("1.) Change Name");
            ConsoleHelper.ColorWriteLine("2.) Change Ingredient");
            ConsoleHelper.ColorWriteLine(ConsoleColor.Red, "6.) Back to Main Menu");
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
            _updateMenuService.UpdateRecipeIngredient();
        }

        private async Task UpdateRecipeName()
        {
            _updateMenuService.UpdateRecipeName();
        }
        
    }
}
