using Models.Enums;
using Newtonsoft.Json;

namespace Models.Sources
{
    public class PeriodicalSource : BaseSource
    {
        public int? YearOfPublication { get; set; }

        public string Publication { get; set; }

        public int? PeriodicSelectionNumber { get; set; }

        public string Pages { get; set; }

        [JsonIgnore]
        public override SourceTypes Type { get; set; } = SourceTypes.Periodical;
    }
}
