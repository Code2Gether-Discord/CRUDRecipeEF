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
            ConsoleHelper.ColorWriteLine(ConsoleColor.Red, "3.) Back to Main Menu");
            Console.WriteLine();
        }
    }
}
