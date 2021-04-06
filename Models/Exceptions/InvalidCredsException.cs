using Models.Consts;

namespace Models.Exceptions
{
    public class InvalidCredsException : BaseException
    {
        public InvalidCredsException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.IncorrectCreds;
        }

    }
}
