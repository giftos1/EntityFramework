
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EFFirstProject
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var db = new BloggingContext();

            // Note: This sample requires the database to be created before running.
            Console.WriteLine($"Database path: {db.DbPath}");

            // Create
            Console.WriteLine("Inserting a new blog");
            db.Add(new Blog { Url = "https://giftos.com/blogs/1" });
            await db.SaveChangesAsync();

            // Read
            Console.WriteLine("Querying for a blog");
            var blog = await db.Blogs
                .OrderBy(blog => blog.BlogId)
                .FirstAsync();

            
            // Update
            Console.WriteLine("Updating the blog and adding a post");
            blog.Url = "https://giftos.com/blogs/1";
            blog.Posts.Add(
                new Post { Title = "Fish and Chips", Content = "Great recipe for fish and chips." }
                );
            //blog.Posts.Add(
            //    new Post { Title = "Roast Beef", Content = "How to cook the perfect roast beef." }
            //    );
            await db.SaveChangesAsync();
            Console.WriteLine(blog.Url);
            Console.WriteLine(blog.Posts.Count);

            // Query for all posts
            var posts = await db.Posts
                .OrderBy(post => post.PostId)
                .ToListAsync();

            Console.WriteLine("All posts:");
            posts.ForEach(post => Console.WriteLine($"{post.PostId}. {post.Title}, {post.Content}"));
            Console.WriteLine();

            // Query for all blogs
            var blogs = await db.Blogs
                .OrderBy(blog => blog.BlogId)
                .ToListAsync();
            Console.WriteLine("All blogs:");
            blogs.ForEach(blog => Console.WriteLine($"{blog.BlogId}. {blog.Url}"));
            Console.WriteLine();
            // Delete
            Console.WriteLine("Delete the blog");
            db.Remove(blog);
            await db.SaveChangesAsync();
        }
    }
}
