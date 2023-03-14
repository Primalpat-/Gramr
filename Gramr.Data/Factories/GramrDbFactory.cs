using Gramr.Core.Interfaces.Data.Factories;
using Gramr.Data.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pluralize.NET;

namespace Gramr.Data.Factories
{
    public class GramrDbFactory : IGramrDbFactory
    {
        private readonly IMongoClient _client;
        private readonly IOptions<DatabaseSettings> _settings;

        public GramrDbFactory(IOptions<DatabaseSettings> settings)
        {
            _settings = settings;

            var mongoSettings = MongoClientSettings.FromConnectionString(settings.Value.ConnectionString);
            mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

            _client = new MongoClient(mongoSettings);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            var pluralizer = new Pluralizer();
            var collectionName = pluralizer.Pluralize(typeof(T).FullName.Split('.').Last());
            return _client.GetDatabase(_settings.Value.DatabaseName).GetCollection<T>(collectionName);
        }
    }
}