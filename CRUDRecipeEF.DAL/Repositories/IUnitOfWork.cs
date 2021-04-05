using System;
using System.Threading.Tasks;

namespace CRUDRecipeEF.DAL.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}