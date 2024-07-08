namespace Habr.Services.Exceptions
{
    public class LogInException : Exception
    {
        public LogInException(string message) : base(message) { }
    }
}
