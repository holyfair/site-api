using System;

namespace Models.Exceptions
{
    public class BaseException : Exception
    {
        protected BaseException(string message)
            :base(message)
        {}

        public string ErrorCode { get; protected set; }
    }
}
