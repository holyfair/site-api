using Models.Enums;

namespace Models.Sources
{
    public interface IBaseSource
    {
        SourceTypes Type { get; set; }
    }
}
