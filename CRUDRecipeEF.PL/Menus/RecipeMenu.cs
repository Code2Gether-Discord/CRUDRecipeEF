using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRUDRecipeEF.BL.DL.Data;

namespace CRUDRecipeEF.PL.Menus
{
    public class RecipeMenu : IRecipeMenu
    {
        private readonly IRecipeService _recipeService;
        private readonly IIngredientService _ingredientService;
        private readonly RecipeContext _recipeContext;

        private enum RecipeMenuOption { InValid = 0, NewRecipe = 1, LookUpRecipe = 2, ShowRecipe = 3, DeleteRecipe = 4, GoBack = 5 };

        public RecipeMenu(IRecipeService recipeService,
            IIngredientService ingredientService,
            RecipeContext recipeContext)
        {
            _recipeService = recipeService;
            _ingredientService = ingredientService;
            _recipeContext = recipeContext;
        }

        public async Task Show()
        {
            ConsoleHelper.DefaultColor = ConsoleColor.Blue;
            ConsoleHelper.ColorWriteLine(ConsoleColor.Yellow, "Recipe Menu");
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
            await ExecuteMenuSelection(choice);
        }

        private async Task ExecuteMenuSelection(RecipeMenuOption option)
        {
            switch (option)
            {
                case RecipeMenuOption.InValid:
                    //TODO throw and exception or something
                    break;
                case RecipeMenuOption.NewRecipe:
                    Console.WriteLine();
                    await NewRecipe();
                    break;
                case RecipeMenuOption.LookUpRecipe:
                    Console.WriteLine();
                    await LookupRecipe();
                    break;
                case RecipeMenuOption.ShowRecipe:
                    Console.WriteLine();
                    await ListRecipe();
                    break;
                case RecipeMenuOption.DeleteRecipe:
                    Console.WriteLine();
                    await DeleteRecipe();
                    break;
                case RecipeMenuOption.GoBack:
                    Console.WriteLine();
                    break;
                default:
                    break;
            }
        }

        private async Task ListRecipe()
        {
            Console.WriteLine();
            ConsoleHelper.ColorWriteLine("Known Recipes: ");

            var result = await _recipeService.GetAllRecipes();
            List<RecipeDetailDTO> recipeList = result.ToList();

            var currentIndex = 0;
            foreach (var recipe in recipeList)
            {
                if (currentIndex % 5 == 0 && currentIndex != 0)
                {
                    Console.WriteLine();
                    ConsoleHelper.ColorWriteLine(ConsoleColor.Yellow, "Press enter for next page.");
                    Console.ReadLine();
                }
                currentIndex++;
                Console.WriteLine(recipe.Name);
            }
            Console.WriteLine();
            await this.Show();
        }

        private async Task DeleteRecipe()
        {
            ConsoleHelper.ColorWrite("What recipe would you like to delete: ");
            var name = Console.ReadLine();

            try
            {
                await _recipeService.DeleteRecipe(name);
            }
            catch (KeyNotFoundException)
            {
                ConsoleHelper.ColorWriteLine(ConsoleColor.DarkYellow, $"{name} does not exist.");
            }

            Console.WriteLine();
            await this.Show();
        }

        private async Task NewRecipe()
        {
            ConsoleHelper.ColorWrite("What recipe would you like to add: ");
            var name = Console.ReadLine();

            RecipeAddDTO recipe = new RecipeAddDTO { Name = name };

            bool another = true;
            List<IngredientAddDTO> ingredients = new List<IngredientAddDTO>();

            while (another)
            {
                ConsoleHelper.ColorWrite("What ingredeient would you like to add: ");
                var input = Console.ReadLine();

                try
                {
                    var ingredient = await _ingredientService.GetIngredientByName(input);
                    var ingredientToAdd = new IngredientAddDTO { Name = ingredient.Name };
                    ingredients.Add(ingredientToAdd);
                }
                catch (KeyNotFoundException)
                {
                    ConsoleHelper.ColorWriteLine(ConsoleColor.DarkYellow, "The ingredient does not exist!");
                    ConsoleHelper.ColorWrite("Would you like to add it? (Y/n): ");
                    var add = Console.ReadLine();

                    if (Char.ToUpperInvariant(add[0]) == 'N')
                    {
                        ConsoleHelper.ColorWriteLine(ConsoleColor.Red, "Recipe not added.");
                        Console.WriteLine();
                        return;
                    }

                    ingredients.Add(new IngredientAddDTO { Name = input });
                }

                ConsoleHelper.ColorWrite("Would you like to add another ingredient? (y/N): ");
                var addAnother = Console.ReadLine();

                if (Char.ToUpperInvariant(addAnother[0]) != 'Y')
                {
                    another = false;
                }
            }

            try
            {
                await _recipeService.AddRecipe(recipe);
                ConsoleHelper.ColorWriteLine(ConsoleColor.Green, $"'{recipe.Name}' has been added.");
            }
            catch (KeyNotFoundException)
            {
                ConsoleHelper.ColorWriteLine(ConsoleColor.DarkYellow, $"{name} already exists.");
            }

            foreach (var ingredient in ingredients)
            {
                try
                {
                    await _recipeService.AddIngredientToRecipe(ingredient, recipe.Name);
                }
                catch (KeyNotFoundException)
                {
                    ConsoleHelper.ColorWriteLine($"'{recipe.Name}' does not exist.");
                }
            }

            Console.WriteLine();
            await this.Show();
        }

        private async Task LookupRecipe()
        {
            ConsoleHelper.ColorWrite("What Recipe would you like to lookup: ");
            var name = Console.ReadLine();

            Console.WriteLine();

            try
            {
                var recipe = await _recipeService.GetRecipeByName(name);
                ConsoleHelper.ColorWriteLine(ConsoleColor.DarkYellow, $"{name} exists.");

                ConsoleHelper.ColorWriteLine(ConsoleColor.DarkYellow, "The Ingredients are: ");
                foreach(var ingredient in recipe.Ingredients)
                {
                    ConsoleHelper.ColorWriteLine(ConsoleColor.White, ingredient.Name);
                }

            }
            catch (KeyNotFoundException)
            {
                ConsoleHelper.ColorWriteLine(ConsoleColor.DarkYellow, $"{name} does not exist.");
            }

            Console.WriteLine();
            await this.Show();
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
