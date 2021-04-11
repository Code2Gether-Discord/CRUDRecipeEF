using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRUDRecipeEF.DAL.Data;
using CRUDRecipeEF.DAL.DTOs;
using CRUDRecipeEF.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDRecipeEF.DAL.Repositories
{
    internal class IngredientRepo : IIngredientRepo
    {
        private readonly RecipeContext _context;
        private readonly IMapper _mapper;

        public IngredientRepo(RecipeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> AddIngredientAsync(Ingredient ingredient)
        {
            await _context.Ingredients.AddAsync(ingredient);
            return ingredient.Name;
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
            _context.Remove(ingredient);
        }

        public async Task<IEnumerable<IngredientDTO>> GetAllIngredientsDTOsAsync()
        {
            return await _context.Ingredients.ProjectTo<IngredientDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public Task<Ingredient> GetIngredientByNameAsync(string name) =>
            _context.Ingredients.SingleOrDefaultAsync(i => i.Name.ToLower() == name.ToLower().Trim());

        public Task<IngredientDTO> GetIngredientDTOByNameAsync(string name)
        {
            return _context.Ingredients.ProjectTo<IngredientDTO>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(i => i.Name == name);
        }

        public Task<bool> IngredientExistsAsync(string name) =>
            _context.Ingredients.AnyAsync(i => i.Name.ToLower() == name.ToLower().Trim());

    }
}