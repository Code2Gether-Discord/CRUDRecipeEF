using System;

namespace CRUDRecipeEF.PL.Menus
{
    public class IngredientMenu : IIngredientMenu
    {
        private enum IngredientMenuOption { InValid = 0, NewIngredient = 1, LookUpIngrediente = 2, ShowIngredient = 3, DeleteIngredient = 4 };

        public void Show()
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
            ExecuteMenuSelection(choice);
        }

        private void ExecuteMenuSelection(IngredientMenuOption option)
        {
            switch (option)
            {
                case IngredientMenuOption.InValid:
                    break;
                case IngredientMenuOption.NewIngredient:
                    break;
                case IngredientMenuOption.LookUpIngrediente:
                    break;
                case IngredientMenuOption.ShowIngredient:
                    break;
                case IngredientMenuOption.DeleteIngredient:
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
