using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CRUDRecipeEF.DAL.Data;
using CRUDRecipeEF.DAL.DTOs;
using CRUDRecipeEF.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDRecipeEF.BL.Services
{
    public class RecipeCategoryService : IRecipeCategoryService
    {
        private readonly RecipeContext _context;
        private readonly IMapper _mapper;

        public RecipeCategoryService(RecipeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> AddCategory(RecipeCategoryDTO categoryAddDTO)
        {
            if (await CategoryExists(categoryAddDTO.Name))
            {
                throw new ArgumentException("Category exists");
            }

            await _context.AddAsync(_mapper.Map<RecipeCategory>(categoryAddDTO));
            await Save();

            return categoryAddDTO.Name;
        }

        public async Task<RecipeCategoryDTO> GetCategoryByName(string name)
        {
            var category = await GetCategoryByNameIfExists(name);

            return _mapper.Map<RecipeCategoryDTO>(category);
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

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private async Task<RecipeCategory> GetCategoryByNameIfExists(string name)
        {
            var category = await _context.RecipeCategories.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower().Trim());
            if (category == null)
            {
                throw new KeyNotFoundException("Category doesn't exist");
            }

            return category;
        }

        private async Task<bool> CategoryExists(string categoryName)
        {
            bool exists = await _context.RecipeCategories.AnyAsync(c => c.Name.ToLower() == categoryName.ToLower().Trim());
            return exists;
            // Not using => makes this easier to debug. For some reason .ToLowerInvariant() does not work in the predicate.
            // Possibly not compatiable with async? May look into this later.
        }
    }
}