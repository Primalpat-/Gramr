using System.Linq.Expressions;
using Gramr.Core.Interfaces.Data.Models;
using Gramr.Core.Interfaces.Data.Services;
using MongoDB.Bson;

namespace Gramr.Logic.Services.Data
{
    public class GenericDataService<T> : IDataService<T> where T : IHavePrimaryKey
    {
        private readonly IDataAccessService<T> _dataAccessService;

        public GenericDataService(IDataAccessService<T> dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public async Task CreateAsync(T model)
        {
            model.Id = ObjectId.GenerateNewId().ToString();
            await _dataAccessService.CreateAsync(model);
        }

        public async Task CreateManyAsync(List<T> models)
        {
            foreach (var model in models)
                model.Id = ObjectId.GenerateNewId().ToString();

            await _dataAccessService.CreateManyAsync(models);
        }

        public async Task<T> GetAsync(string id)
        {
            return await _dataAccessService.GetAsync(id);
        }

        public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>>? filter = null)
        {
            return await _dataAccessService.QueryAsync(filter);
        }

        public async Task UpdateAsync(T model)
        {
            await _dataAccessService.UpdateAsync(model);
        }

        public async Task DeleteAsync(string id)
        {
            await _dataAccessService.DeleteAsync(id);
        }
    }
}