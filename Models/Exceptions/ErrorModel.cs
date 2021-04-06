namespace Models.Exceptions
{
    public class ErrorModel
    {
        public string ErrorCode { get; set; }
        public string Error { get; set; }
        public string StackTrace { get; set; }
    }
}
