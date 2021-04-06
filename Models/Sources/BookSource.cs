using Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Models.Sources
{
    public class BookSource : BaseSource
    {
        public string PlaceOfPublication { get; set; }

        public string PublishingHouse { get; set; }

        public int? YearOfPublication { get; set; }

        public string NumberOfPages { get; set; }

        public string PublishingName { get; set; }

        public string Series { get; set; }

        public int? PeriodicSelectionNumber { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public PublicationNumberTypes PublicationNumberType { get; set; }

        [JsonIgnore]
        public override SourceTypes Type { get; set; } = SourceTypes.Book;
    }
}
