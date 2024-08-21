namespace Habr.DataAccess.Constraints
{
    public class ConstraintValue
    {
        public const int UserNameMaxLength = 100;
        public const int UserEmailMaxLength = 200;
        public const int UserPasswordHashMaxLength = 64;
        public const int UserSaltMaxLength = 24;

        public const int PostTitleMaxLength = 200;
        public const int PostTextMaxLength = 2000;

        public const int CommentTextMaxLength = 1000;

        public const int PostRatingStarsMin = 1;
        public const int PostRatingStarsMax = 5;
    }
}
