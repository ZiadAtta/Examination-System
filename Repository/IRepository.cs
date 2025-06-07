using Examination_System.Models;
using System.Linq.Expressions;

namespace Examination_System.Repository
{
    public interface IRepository<Entity> where Entity : BaseEntity
    {
        Task<Entity> GetByIdAsync(int id);
        IQueryable<Entity> GetAll();
        Task<bool> AddAsync(Entity entity);
        Task<bool> UpdateAsync(Entity entity);
        Task<bool> HardDeleteAsync(int id);
        Task<bool> SoftDeleteAsync(int id);
        Task SaveChangesAsync();
        Task<bool> ExistsAsync(int id);
        Task<IQueryable<Entity>> GetAsync(Expression<Func<Entity, bool>> predicate);
        Task<bool> Update(Entity entity);
        Task<bool> SaveIncludeAsync(Entity entity,params string[] Properties);
    }
}
