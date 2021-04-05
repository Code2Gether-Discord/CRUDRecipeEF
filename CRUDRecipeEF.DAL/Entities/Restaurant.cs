using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDRecipeEF.DAL.Entities
{
    public class Restaurant
    {
        private string _name;

        public int Id { get; set; }
        [Required]
        public string Name { get => _name; set => _name = value.Trim(); }
        public List<Menu> Menus { get; set; } = new List<Menu>();
    }
}
