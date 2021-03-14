using Microsoft.Extensions.Logging;
using System;

namespace CRUDRecipeEF.PL.Menus
{
    public class MainMenu : IMainMenu
    {
        private readonly IIngredientMenu _ingredientMenu;
        private readonly IRecipeMenu _recipeMenu;
        private readonly ILogger _logger;

        private enum MainMenuOption { InValid = 0, RecipeMenu = 1, IngredientMenu = 2, Quit = 3 };

        public MainMenu(IIngredientMenu ingredientMenu,
            IRecipeMenu recipeMenu,
            ILogger<MainMenu> logger)
        {
            _ingredientMenu = ingredientMenu;
            _recipeMenu = recipeMenu;
            _logger = logger;
        }

        public void Show()
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

                while (!valid)
                {
                    ConsoleHelper.ColorWrite(ConsoleColor.Yellow, "Please select an option: ");
                    input = Console.ReadLine();

                    valid = ConsoleHelper.ValidateInt(input, (int)MainMenuOption.RecipeMenu, (int)MainMenuOption.Quit, out option);

                    if (!Enum.IsDefined(typeof(MainMenuOption), option))
                    {
                        _logger.LogWarning("Option is not in enum");
                        valid = false;
                    }

                }

                MainMenuOption choice = (MainMenuOption)option;
                ExecuteMenuSelection(choice);
            }
        }

        private void ExecuteMenuSelection(MainMenuOption option)
        {
            switch (option)
            {
                case MainMenuOption.InValid:
                    _logger.LogWarning("Attempted to execute invalid menu selection");
                    break;
                case MainMenuOption.RecipeMenu:
                    Console.WriteLine();
                    _recipeMenu.Show();
                    break;
                case MainMenuOption.IngredientMenu:
                    Console.WriteLine();
                    _ingredientMenu.Show();
                    break;
                case MainMenuOption.Quit:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}