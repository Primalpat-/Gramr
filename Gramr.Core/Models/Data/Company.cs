using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Gramr.Core.Interfaces.Data.Models;

namespace Gramr.Core.Models.Data
{
    public class Company : IHavePrimaryKey
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ticker is required.")]
        public string Ticker { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public bool Active { get; set; } = true;
    }
}
