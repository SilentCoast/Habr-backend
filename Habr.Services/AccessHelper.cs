using Habr.DataAccess.Enums;
using Habr.Services.Resources;

namespace Habr.Services
{
    public static class AccessHelper
    {
        public static void CheckPostAccess(int userId, int postOwnerId, RoleType role)
        {
            CheckAccess(userId, postOwnerId, role, ExceptionMessage.AcccessDeniedWrongPostOwner);
        }

        public static void CheckCommentAccess(int userId, int commentOwnerId, RoleType role)
        {
            CheckAccess(userId, commentOwnerId, role, ExceptionMessage.AccessDeniedWrongCommentOwner);
        }

        private static void CheckAccess(int userId, int ownerId, RoleType role, string exceptionMessage)
        {
            if (role != RoleType.Admin && userId != ownerId)
            {
                throw new UnauthorizedAccessException(exceptionMessage);
            }
        }
    }
}
