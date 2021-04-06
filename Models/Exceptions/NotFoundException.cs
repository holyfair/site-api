using Models.Consts;

namespace Models.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.NotFound;
        }
    }
}
