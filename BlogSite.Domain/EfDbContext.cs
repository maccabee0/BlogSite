using System.Data.Entity;

using BlogSite.Domain.Entities;

namespace BlogSite.Domain
{
    public class EfDbContext : DbContext
    {
        public IDbSet<User> Users { get; set; }

        public IDbSet<Blog> Blogs { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Role> Roles { get; set; }

        //public DbSet<Media> Medias { get; set; }

        //public DbSet<Tag> Tags { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasMany(e => e.Roles).WithMany(c => c.Users);
            modelBuilder.Entity<Blog>().HasRequired(u => u.User).WithMany(b => b.Blogs);
            modelBuilder.Entity<User>().HasMany(u => u.Comments).WithRequired(c => c.User);
            modelBuilder.Entity<Blog>().HasMany(b => b.Comments).WithRequired(c => c.Blog);
            //modelBuilder.Entity<Blog>().HasMany(e => e.Tags).WithMany(c => c.Blogs);
            base.OnModelCreating(modelBuilder);
        }
    }
}
