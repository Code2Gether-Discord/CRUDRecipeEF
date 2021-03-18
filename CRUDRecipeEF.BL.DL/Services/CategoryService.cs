using AutoMapper;
using CRUDRecipeEF.BL.DL.Data;
using CRUDRecipeEF.BL.DL.DTOs;
using CRUDRecipeEF.BL.DL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.DL.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly RecipeContext _context;
        private readonly IMapper _mapper;

        public CategoryService(RecipeContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private async Task<Category> GetCategoryByNameIfExists(string name)
        {
            var category = await _context.Category.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower().Trim());
            if (category == null)
            {
                throw new KeyNotFoundException("Category doesn't exist");
            }

            return category;
        }

        

        public async Task<string> AddCategory(CategoryAddDTO categoryAddDTO)
        {
            if (await CategoryExists(categoryAddDTO.Name))
            {
                throw new ArgumentException("Category exists");
            }

            await _context.AddAsync(_mapper.Map<Categories>(categoryAddDTO));
            await Save();

            return categoryAddDTO.Name;
        }

        private async Task<bool> CategoryExists(string categoryName)
        {
            bool exists = await _context.Categories.AnyAsync(c => c.Name.ToLower() == categoryName.ToLower().Trim());
            return exists;
            // Not using => makes this easier to debug. For some reason .ToLowerInvariant() does not work in the predicate.
            // Possibly not compatiable with async? May look into this later.
        }

        

        public async Task<CategoryDetailDTO> GetCategoryByName(string name)
        {
            var category = await GetCategoryByNameIfExists(name);

            return _mapper.Map<CategoryDetailDTO>(category);
        }

       
        public async Task DeleteCategory(string name)
        {
            var category = await GetCategoryByNameIfExists(name);

            _context.Remove(category);
            await Save();
        }

       

        public async Task<string> AddRecipeToCategory(RecipeDTO recipeAddDTO, string categoryName)
        {
            var category = await GetCategoryByNameIfExists(categoryName);
            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(x => x.Name.ToLower() == recipeAddDTO.Name.ToLower().Trim());

            if (recipe == null)
            {
                recipe.Category = category; // need to solve 
            }
            else
            {
                recipe.Category = category; // same
            }

            await Save();

            return categoryName;
        }
    }
}
