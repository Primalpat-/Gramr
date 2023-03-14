namespace Gramr.Core.Models.Dtos
{
    public class AggregateDataDto
    {
        public string ticker { get; set; }
        public int queryCount { get; set; }
        public int resultsCount { get; set; }
        public bool adjusted { get; set; }
        public List<AggregateDto> results { get; set; } = new();
        public string status { get; set; }
        public string request_id { get; set; }
        public int count { get; set; }
    }
}
