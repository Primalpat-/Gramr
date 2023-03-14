namespace Gramr.Core.Interfaces.Data.Factories
{
    using MongoDB.Driver;
    
    public interface IGramrDbFactory
    {
        IMongoCollection<T> GetCollection<T>();
    }
}