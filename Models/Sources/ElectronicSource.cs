using System.ComponentModel.DataAnnotations;
using Models.Enums;
using Newtonsoft.Json;

namespace Models.Sources
{
    public class ElectronicSource : BaseSource
    {
        public int? YearOfPublication { get; set; }

        public string Publication { get; set; }

        [Required]
        public string LinkToSource { get; set; }

        [JsonIgnore]
        public override SourceTypes Type { get; set; } = SourceTypes.Electronic;
    }
}
