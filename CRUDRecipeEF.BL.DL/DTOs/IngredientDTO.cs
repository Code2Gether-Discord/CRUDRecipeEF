using System.ComponentModel.DataAnnotations;

namespace CRUDRecipeEF.BL.DL.DTOs
{
    public class IngredientDTO
    {
        public int Id { get; set; }

        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        //we dont have a list of recipes here because a recipe has ingredients, the ingredient doesnt have recipes it belongs to them
    }
}