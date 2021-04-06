using Models.Enums;

namespace Models.Sources
{
    public class AbstractDissertationSource : DissertationSource
    {
        public string SpecialtyName { get; set; }

        public override SourceTypes Type { get; set; } = SourceTypes.AbstractOfDissertation;

        public override string GetScientificDegree()
        {
            var result = base.GetScientificDegree();
            return result.Replace(" : дис.", " : автореф. дис. на здобуття наук. ступеня"); ;
        }

        public override string GetSpecialty()
        {
            if(string.IsNullOrEmpty(SpecialtyName) && SpecialtyCode == null)
            {
                return string.Empty;
            }
            var result = " : спец. ";

            if(SpecialtyCode != null)
            {
                result += SpecialtyCode;
            }
            if (!string.IsNullOrEmpty(SpecialtyName))
            {
                result += $" \"{SpecialtyName}\"";
            }

            return result;
        }
    }
}
