using Habr.DataAccess.Entities;

namespace Habr.DataAccess.Repositories
{
    public interface ICommentRepository
    {
        Task AddCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
    }
}