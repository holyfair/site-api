using Models.Consts;

namespace Models.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.BadRequest;
        }
    }
}
