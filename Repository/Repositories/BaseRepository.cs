using Models.Exceptions;

namespace Repository.Repositories
{
    public abstract class BaseRepository
    {
        protected void ValidateIdentity(object item, object key)
        {
            if(item is null)
            {
                throw new NotFoundException($"Cant find item by key: {key}");
            }
        }
    }
}
