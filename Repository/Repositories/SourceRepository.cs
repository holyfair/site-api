using Microsoft.EntityFrameworkCore;
using Models.Shared;
using Repository.Context;
using Repository.DatabaseModels;
using Repository.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SourceRepository : BaseRepository
    {
        private DbSet<SourceRecord> sources;

        public SourceRepository(AppDbContext context)
        {
            sources = context.Sources;
        }

        public async Task CreateSourceAsync(SourceRecord source)
        {
            await sources.AddAsync(source);
        }

        public async Task<PagedResult<SourceRecord>> GetSourcesAsync(GetListQuery query, string userEmail)
        {
            var result = await GetAll().Where(x => x.User.Email == userEmail).ToPagedResultAsync(query);
            return result;
        }

        private IQueryable<SourceRecord> GetAll()
        {
            return sources.Include(x => x.User);
        }
    }
}
