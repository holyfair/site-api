using Models.Shared;
using Models.Sources;
using Repository.DatabaseModels;
using System.Threading.Tasks;

namespace Services.Interfaceses
{
    public interface ISourceService
    {
        Task<string> CreateSourceAsync(IBaseSource source, string userEmail = null);

        Task<PagedResult<SourceRecord>> GetSourceAsync(GetListQuery query, string userEmail);
    }
}
