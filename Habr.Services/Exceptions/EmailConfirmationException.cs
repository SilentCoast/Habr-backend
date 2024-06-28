namespace Habr.Services.Exceptions
{
    public class EmailConfirmationException : Exception
    {
        public EmailConfirmationException(string message = "Email confirmation failed") : base(message) { }
    }
}
