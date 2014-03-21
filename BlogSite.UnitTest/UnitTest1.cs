using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;

using BlogSite.Domain.Entities;
using NUnit.Framework;

using BlogSite.Domain;
using BlogSite.Services.Concrete;

namespace BlogSite.UnitTest
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestEdmxContainer()
        {
            //EdmxContextContainer container = new EdmxContextContainer();
            //var blogDs = container.Blogs;
            //var blogs = blogDs.ToList();
            //Blog blog = blogs[0];
            //Assert.IsNotNull(blog);
        }

        [Test]
        public void TestServiceRead()
        {
            SiteService repo=new SiteService(new UserRepository(), new CommentRepository(), new BlogRepository());
            IQueryable<Blog> blogs = repo.Blogs;
            Blog blog = blogs.FirstOrDefault(b => b.BlogId == 1);
            Assert.IsNotNull(blog.User);
        }

        [Test]
        public void TestUserRole()
        {
            SiteService repo= new SiteService(new UserRepository(), new CommentRepository(), new BlogRepository());
            IQueryable<User> users = repo.Users;
            User user = users.FirstOrDefault(u => u.UserId == 7);
            Assert.IsNotEmpty(user.Roles);
        }

        string Serialize<T>(MediaTypeFormatter formatter, T value)
        {
            // Create a dummy HTTP Content.
            Stream stream = new MemoryStream();
            var content = new StreamContent(stream);
            /// Serialize the object.
            formatter.WriteToStreamAsync(typeof(T), value, stream, content, null).Wait();
            // Read the serialized string.
            stream.Position = 0;
            return content.ReadAsStringAsync().Result;
        }

        T Deserialize<T>(MediaTypeFormatter formatter, string str) where T : class
        {
            // Write the serialized string to a memory stream.
            Stream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            // Deserialize to an object of type T
            return formatter.ReadFromStreamAsync(typeof(T), stream, null, null).Result as T;
        }

        [Test]
        public void TestSerialization()
        {
            var marcus = new User() {UserName = "Marcus", Blogs = new List<Blog>(), };
            var value = new Blog()
                {
                    Subject = "ahppyness",
                    PostDate = DateTime.Now,
                    Text = "adgflhj mwerij jas",
                    User = marcus,
                };
            marcus.Blogs.Add(value);

            var xml = new XmlMediaTypeFormatter();
            string str = Serialize(xml, value);

            var json = new JsonMediaTypeFormatter();
            str = Serialize(json, value);

            // Round trip
            Blog person2 = Deserialize<Blog>(json, str);
        }
    }
}
