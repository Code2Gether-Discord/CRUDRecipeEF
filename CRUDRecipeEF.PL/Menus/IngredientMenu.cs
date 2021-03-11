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
        private readonly IIngredientService _ingredientService;
        private readonly int _ingredientsPerPage = 8;

        private enum IngredientMenuOption { InValid = 0, NewIngredient = 1, LookUpIngredient = 2, ShowIngredient = 3, DeleteIngredient = 4, GoBack = 5 };

        public IngredientMenu(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public async Task Show()
        {
            ConsoleHelper.DefaultColor = ConsoleColor.Blue;
            ConsoleHelper.ColorWriteLine(ConsoleColor.Yellow, "Ingredient Menu");
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

                valid = ConsoleHelper.ValidateInt(input, (int)IngredientMenuOption.NewIngredient, (int)IngredientMenuOption.GoBack, out option);

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
                    //TODO throw an exception or something
                    break;
                case IngredientMenuOption.NewIngredient:
                    await NewIngredient();
                    break;
                case IngredientMenuOption.LookUpIngredient:
                    await LookupIngredient();
                    break;
                case IngredientMenuOption.ShowIngredient:
                    await ListIngredients();
                    break;
                case IngredientMenuOption.DeleteIngredient:
                    await DeleteIngredient();
                    break;
                case IngredientMenuOption.GoBack:
                    Console.WriteLine();
                    break;
                default:
                    break;
            }
        }

        private async Task LookupIngredient()
        {
            ConsoleHelper.ColorWrite("What ingredient would you like to lookup: ");
            var name = Console.ReadLine();

            Console.WriteLine();

            try
            {
                var ingredient = await _ingredientService.GetIngredientByName(name);
                ConsoleHelper.ColorWriteLine(ConsoleColor.DarkYellow, $"{name} exists.");
            }
            catch(KeyNotFoundException)
            {
                ConsoleHelper.ColorWriteLine(ConsoleColor.DarkYellow,$"{name} does not exist.");
            }

            Console.WriteLine();
            await this.Show();
        }

        private async Task DeleteIngredient()
        {
            ConsoleHelper.ColorWrite("What ingredient would you like to delete: ");
            var name = Console.ReadLine();

            try
            {
                await _ingredientService.DeleteIngredient(name);
            }
            catch (KeyNotFoundException)
            {
                ConsoleHelper.ColorWriteLine(ConsoleColor.DarkYellow, $"{name} does not exist.");
            }

            Console.WriteLine();
            await this.Show();
        }

        private async Task NewIngredient()
        {
            ConsoleHelper.ColorWrite("What ingredient would you like to add: ");
            var name = Console.ReadLine();

            IngredientAddDTO newIngreditent = new IngredientAddDTO { Name = name };

            try
            {
                await _ingredientService.AddIngredient(newIngreditent);
            }
            catch (KeyNotFoundException)
            {
                ConsoleHelper.ColorWriteLine(ConsoleColor.DarkYellow, $"{name} already exists.");
            }

            Console.WriteLine();
            await this.Show();
        }

        private async Task ListIngredients()
        {
            Console.WriteLine();
            ConsoleHelper.ColorWriteLine("Known Ingredients: ");

            var result = await _ingredientService.GetAllIngredients();
            List<IngredientDetailDTO> ingredientList = result.ToList();

            for (int i = 0; i < ingredientList.Count; i++)
            {
                if (i % _ingredientsPerPage == 0 && i != 0)
                {
                    Console.WriteLine();
                    ConsoleHelper.ColorWriteLine(ConsoleColor.Yellow,"Press enter for next page.");
                    Console.ReadLine();
                }
                Console.WriteLine(ingredientList[i].Name);
            }
            Console.WriteLine();
            await this.Show();
        }
    }
}
