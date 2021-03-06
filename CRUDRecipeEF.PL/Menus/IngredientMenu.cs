using System;

namespace CRUDRecipeEF.PL.Menus
{
    public class IngredientMenu : IIngredientMenu
    {
        private enum RecipeMenuOption { InValid = 0, NewIngredient = 1, LookUpIngrediente = 2, ShowIngredient = 3, DeleteIngredient = 4 };

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
            ConsoleHelper.ColorWriteLine(ConsoleColor.Red, "3.) Back to Main Menu");
            Console.WriteLine();
        }
    }
}
