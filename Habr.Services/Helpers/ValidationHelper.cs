namespace Habr.Services.Helpers
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Validates that <paramref name="value"/> is more than 0. 
        /// And if it is not throws <see cref="ArgumentException"/>(<paramref name="exceptionMessage"/>)
        /// </summary>
        public static void ValidateIntMoreThan0(int value, string exceptionMessage)
        {
            if (value < 1)
            {
                throw new ArgumentException(exceptionMessage);
            }
        }
    }
}
