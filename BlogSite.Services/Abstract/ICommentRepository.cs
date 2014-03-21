using System.Linq;

using BlogSite.Domain.Entities;

namespace BlogSite.Services.Abstract
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }

        void SaveComment(Comment comment);

        void DeleteComment(Comment comment);

        Comment GetComment(int id);

        IQueryable<Comment> GetCommentsByBlog(int blogId);
    }
}
