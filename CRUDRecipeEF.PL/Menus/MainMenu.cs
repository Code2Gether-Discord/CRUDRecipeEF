using System;

namespace CRUDRecipeEF.PL.Menus
{
    public class MainMenu : IMainMenu
    {
        private readonly IIngredientMenu _ingredientMenu;
        private readonly IRecipeMenu _recipeMenu;

        private enum MainMenuOption { InValid = 0, RecipeMenu = 1, IngredientMenu = 2, Quit = 3 };

        public MainMenu(IIngredientMenu ingredientMenu,
            IRecipeMenu recipeMenu)
        {
            _ingredientMenu = ingredientMenu;
            _recipeMenu = recipeMenu;
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

        private void ExecuteMenuSelection(MainMenuOption option)
        {
            switch (option)
            {
                case MainMenuOption.InValid:
                    //TODO throw and exception or something
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