using Models.Consts;
using Models.Enums;
using Newtonsoft.Json;

namespace Models.Sources
{
    public class DissertationSource : BaseSource
    {
        public string ScientificDegreeName { get; set; }

        public string ScientificDegreeSpecialty { get; set; }

        public int? SpecialtyCode { get; set; }

        public string PlaceOfPublication { get; set; }

        public int? YearOfPublication { get; set; }

        public int? NumberOfPages { get; set; }

        public virtual string GetScientificDegree()
        {
            if(string.IsNullOrEmpty(ScientificDegreeName) && string.IsNullOrEmpty(ScientificDegreeSpecialty))
            {
                return string.Empty;
            }
            var result = " : дис. ";
            if(!string.IsNullOrEmpty(ScientificDegreeName))
            {
                result += ScientificDegrees.ScientificDegreeNames[ScientificDegreeName];
            }
            if (!string.IsNullOrEmpty(ScientificDegreeSpecialty))
            {
                result += $" {ScientificDegrees.ScientificDegreeSpecialties[ScientificDegreeSpecialty]}";
            }

            return result;
        }

        public virtual string GetSpecialty()
        {
            return SpecialtyCode == null ? string.Empty : $" : {SpecialtyCode}";
        }

        [JsonIgnore]
        public override SourceTypes Type { get; set; } = SourceTypes.Dissertation;
    }
}
