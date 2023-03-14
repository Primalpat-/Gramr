using System.Linq.Expressions;
using Gramr.Core.Interfaces.Data.Factories;
using Gramr.Core.Interfaces.Data.Models;
using Gramr.Core.Interfaces.Data.Services;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;

namespace Gramr.Data.Services
{
    public class DataAccessService<T> : IDataAccessService<T> where T : IHavePrimaryKey
    {
        private readonly IMongoCollection<T> _collection;

        public DataAccessService(IGramrDbFactory db)
        {
            _collection = db.GetCollection<T>();
        }

        public async Task CreateAsync(T model)
        {
            await _collection.InsertOneAsync(model);
        }

        public async Task CreateManyAsync(List<T> models)
        {
            await _collection.InsertManyAsync(models);
        }

        public async Task<T> GetAsync(string id)
        {
            var results = await _collection.FindAsync(x => x.Id == id);
            return await results.SingleAsync();
        }

        public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>>? filter = null)
        {
            IAsyncCursor<T> results;

            if (filter == null)
                results = await _collection.FindAsync(_ => true);
            else
                results = await _collection.FindAsync(filter);

            return await results.ToListAsync();
        }

        public async Task UpdateAsync(T model)
        {
            await _collection.FindOneAndReplaceAsync(x => x.Id == model.Id, model);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
