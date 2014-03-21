using System.Data;
using System.Data.Entity;
using System.Linq;

using BlogSite.Domain;
using BlogSite.Domain.Entities;
using BlogSite.Services.Abstract;

namespace BlogSite.Services.Concrete
{
    public class CommentRepository : ICommentRepository
    {
        private EfDbContext _context = new EfDbContext();

        public IQueryable<Comment> Comments
        {
            get
            {
                return _context.Comments.Include(c => c.Blog).Include(c => c.User);
            }
        }

        public void SaveComment(Comment comment)
        {
            if (comment.CommentId == 0)
            {
                _context.Comments.Add(comment);
            }
            else
            {
                _context.Entry(comment).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }

        public Comment GetComment(int id)
        {
            return Comments.Where(c => c.CommentId == id).FirstOrDefault();
        }

        public IQueryable<Comment> GetCommentsByBlog(int blogId)
        {
            return Comments.Where(c => c.BlogId == blogId);
        }

        public void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
}
