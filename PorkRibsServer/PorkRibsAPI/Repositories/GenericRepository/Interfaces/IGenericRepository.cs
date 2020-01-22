using System;
using System.Linq;
using System.Threading.Tasks;

namespace PorkRibsAPI.Repositories.GenericRepository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task Create(TEntity item);
        Task<TEntity> FindById(int id);
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get(Func<TEntity, bool> predicate);
        Task Remove(TEntity item);
        Task Update(TEntity item);
    }
}
