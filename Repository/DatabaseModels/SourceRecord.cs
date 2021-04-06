using Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Repository.DatabaseModels
{
    public class SourceRecord
    {
        public int Id { get; set; }

        public string Content { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SourceTypes Type { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
