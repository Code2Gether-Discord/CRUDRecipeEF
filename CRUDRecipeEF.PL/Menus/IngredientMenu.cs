using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDRecipeEF.PL.Menus
{
    public class IngredientMenu : IIngredientMenu
    {
        private readonly IIngredientService ingredientService;

        private enum IngredientMenuOption { InValid = 0, NewIngredient = 1, LookUpIngredient = 2, ShowIngredient = 3, DeleteIngredient = 4 };

        public IngredientMenu(IIngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        public async Task Show()
        {
            ConsoleHelper.DefaultColor = ConsoleColor.Blue;
            ConsoleHelper.ColorWriteLine(ConsoleColor.Yellow, "RecipeMenu");
            Console.WriteLine();
            ConsoleHelper.ColorWriteLine("1.) New Ingredient");
            ConsoleHelper.ColorWriteLine("2.) Lookup Ingredient");
            ConsoleHelper.ColorWriteLine("3.) Show Ingredient List");
            ConsoleHelper.ColorWriteLine("4.) Delete Ingredient");
            Console.WriteLine();
            ConsoleHelper.ColorWriteLine(ConsoleColor.Red, "5.) Back to Main Menu");
            Console.WriteLine();

            string input = string.Empty;
            int option = 0;
            bool valid = false;

            while (!valid)
            {
                ConsoleHelper.ColorWrite(ConsoleColor.Yellow, "Please select an option: ");
                input = Console.ReadLine();

                valid = validateInt(input, 1, 5, out option);

                if (!Enum.IsDefined(typeof(IngredientMenuOption), option))
                {
                    // Not in the enum - log here if desired
                    valid = false;
                }

            }

            IngredientMenuOption choice = (IngredientMenuOption)option;
            await ExecuteMenuSelection(choice);
        }

        private async Task ExecuteMenuSelection(IngredientMenuOption option)
        {
            switch (option)
            {
                case IngredientMenuOption.InValid:
                    break;
                case IngredientMenuOption.NewIngredient:
                    NewIngredient();
                    break;
                case IngredientMenuOption.LookUpIngredient:
                    break;
                case IngredientMenuOption.ShowIngredient:
                    await ListIngredients();
                    break;
                case IngredientMenuOption.DeleteIngredient:
                    break;
                default:
                    break;
            }
        }

        private void NewIngredient()
        {
            ConsoleHelper.ColorWrite("What ingredient would you like to add: ");
            var name = Console.ReadLine();

            IngredientAddDTO newIngreditent = new IngredientAddDTO { Name = name };

            ingredientService.AddIngredient(newIngreditent);
        }

        private async Task ListIngredients()
        {
            var result = await ingredientService.GetAllIngredients();
            List<IngredientDetailDTO> ingredientList = result.ToList();

            var currentIndex = 0;
            foreach(var ingredient in ingredientList)
            {
                if (currentIndex % 5 == 0 && currentIndex != 0)
                {
                    Console.WriteLine("Press enter for next page.");
                    Console.ReadLine();
                }

                Console.WriteLine(ingredient.Name);
            }
        }

        private bool validateInt(string input, int min, int max, out int result)
        {
            if (!int.TryParse(input, out result))
            {
                // Not a valid int - log error here if desired
                return false;
            }

            if (result > max || result < min)
            {
                // Outside expected range - log error here if desired
                return false;
            }

            return true;
        }
    }
}
