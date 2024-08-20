using Asp.Versioning;

namespace Habr.WebApp
{
    public static class ApiVersions
    {
        public static ApiVersion ApiVersion1 => new(1, 0);
        public static ApiVersion ApiVersion2 => new(2, 0);
    }
}
