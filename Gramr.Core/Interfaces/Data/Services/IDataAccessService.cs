using System.Linq.Expressions;

namespace Gramr.Core.Interfaces.Data.Services
{
    public interface IDataAccessService<T>
    {
        Task CreateAsync(T model);
        Task CreateManyAsync(List<T> models);
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>>? filter = null);
        Task UpdateAsync(T model);
        Task DeleteAsync(string id);
    }
}