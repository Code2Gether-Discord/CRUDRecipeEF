using AutoMapper;
using CRUDRecipeEF.DAL.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDRecipeEF.BL.Services
{
    public class UpdateRecipeMenuService : IUpdateRecipeMenuService
    {
        private readonly RecipeContext _context;
        private readonly ILogger<UpdateRecipeMenuService> _logger;
        private readonly IMapper _mapper;

        public UpdateRecipeMenuService(RecipeContext context,
            IMapper mapper,
            ILogger<UpdateRecipeMenuService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public Task UpdateRecipeIngredient()
        {
            throw new NotImplementedException();
        }

        public Task UpdateRecipeName()
        {
            throw new NotImplementedException();
        }
    }
}
