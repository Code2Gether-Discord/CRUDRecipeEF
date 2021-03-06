using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Services;
using System;

namespace CRUDRecipeEF.PL.Menus
{
    public class MainMenu
    {
        private readonly IRecipeService _recipeService;

        public MainMenu(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public enum MainMenuOption { InValid = 0, RecipeMenu = 1, IngredientMenu = 2, Quit = 3};

        public void Run()
        {
            while (true)
            {
                ConsoleHelper.DefaultColor = ConsoleColor.Blue;
                ConsoleHelper.ColorWriteLine(ConsoleColor.Yellow, "Welcome to CRUDRecipeEF");
                Console.WriteLine();
                ConsoleHelper.ColorWriteLine("1.) Recipe Menu");
                ConsoleHelper.ColorWriteLine("2.) Ingredient Menu");
                Console.WriteLine();
                ConsoleHelper.ColorWriteLine(ConsoleColor.Red, "3.) Quit");
                Console.WriteLine();


                string input = string.Empty;
                int option = 0;
                bool valid = false;

                while(!valid)
                {
                    ConsoleHelper.ColorWrite(ConsoleColor.Yellow,"Please select an option: ");
                    input = Console.ReadLine();

                    valid = validateInt(input, 1, 3, out option);

                    if (!Enum.IsDefined(typeof(MainMenuOption), option))
                    {
                        // Not in the enum - log here if desired
                        valid = false;
                    }

                }

                MainMenuOption choice = (MainMenuOption)option;
                ExecuteMenuSelection(choice);
            }

            //WriteLine("Hello there!");
            //_recipeService.AddRecipe(new RecipeAddDTO() { Name = "brains" });
            //WriteLine(_recipeService.GetRecipeByName("human brains"));
            //var recipes = _recipeService.GetAllRecipes();
            //ReadLine();
        }

        private static void ExecuteMenuSelection(MainMenuOption option)
        {
            switch (option)
            {
                case MainMenuOption.RecipeMenu:
                    break;
                case MainMenuOption.IngredientMenu:
                    break;
                case MainMenuOption.Quit:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        private static bool validateInt(string input, int min, int max, out int result)
        {
            if(!int.TryParse(input, out result))
            {
                // Not a valid int - log error here if desired
                return false;
            }

            if(result > max || result < min)
            {
                // Outside expected range - log error here if desired
                return false;
            }

            return true;
        }
    }
}