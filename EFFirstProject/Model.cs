using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

/*Tip: This application intentionally keeps things simple for clarity. Connection strings should not be stored in the code for production applications. 
 * You may also want to split each C# class into its own file.*/

namespace EFFirstProject
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; } // Represents the Blogs table in the database
        public DbSet<Post> Posts { get; set; } // Represents the Posts table in the database

        public string DbPath { get; } // Property to hold the database path

        public BloggingContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData; // reference to the local application data folder
            var path = Environment.GetFolderPath(folder); // Get the path to the local application data folder as a string
            DbPath = System.IO.Path.Join(path, "blogging.db:"); // Combine the folder path with the database file name
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}"); // Configure the context to use SQLite with the specified database path
        }
    }

    public class  Blog
    {
        public int BlogId { get; set; } // Entity Framework will treat this as the primary key due to the naming convention classNameId
        public string Url { get; set; }
        public List<Post> Posts { get; } = new();
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
