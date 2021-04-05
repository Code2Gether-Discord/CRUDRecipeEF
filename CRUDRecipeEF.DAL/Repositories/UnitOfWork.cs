using System.Threading.Tasks;
using CRUDRecipeEF.DAL.Data;

namespace CRUDRecipeEF.DAL.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly RecipeContext _context;

        public UnitOfWork(RecipeContext context)
        {
            _context = context;
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}