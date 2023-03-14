using Gramr.Core.Interfaces.Data.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gramr.Core.Models.Data
{
    public class MarketAggregate : IHavePrimaryKey
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? CompanyId { get; set; }

        public DateTime Timestamp { get; set; }
        public int Transactions { get; set; }
        public long Volume { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Average { get; set; }
    }
}