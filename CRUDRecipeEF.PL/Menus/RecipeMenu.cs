using System;


namespace CRUDRecipeEF.PL.Menus
{
    public class RecipeMenu : IRecipeMenu
    {
        private enum RecipeMenuOption { InValid = 0, NewRecipe = 1, LookUpRecipe = 2, ShowRecipe = 3, DeleteRecipe = 4 };

        public void Show()
        {
            ConsoleHelper.DefaultColor = ConsoleColor.Blue;
            ConsoleHelper.ColorWriteLine(ConsoleColor.Yellow, "RecipeMenu");
            Console.WriteLine();
            ConsoleHelper.ColorWriteLine("1.) New Recipe");
            ConsoleHelper.ColorWriteLine("2.) Lookup Recipe");
            ConsoleHelper.ColorWriteLine("3.) Show Recipe List");
            ConsoleHelper.ColorWriteLine("4.) Delete Recipe");
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

                if (!Enum.IsDefined(typeof(RecipeMenuOption), option))
                {
                    // Not in the enum - log here if desired
                    valid = false;
                }

            }

            RecipeMenuOption choice = (RecipeMenuOption)option;
            ExecuteMenuSelection(choice);
        }

        private void ExecuteMenuSelection(RecipeMenuOption option)
        {
            switch (option)
            {
                case RecipeMenuOption.InValid:
                    //TODO throw and exception or something
                    break;
                case RecipeMenuOption.NewRecipe:
                    break;
                case RecipeMenuOption.LookUpRecipe:
                    break;
                case RecipeMenuOption.ShowRecipe:
                    break;
                case RecipeMenuOption.DeleteRecipe:
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
