using EFCoreApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();



//WARNING Applying migrations modifies the database, so you must
//always be aware of data loss. If you remove a table from the database using a migration and then roll back the migration, the table will be re-created, but the data it previously contained will be gone forever!

//Applying migrations to a database creates the database if it doesn’t exist and updates the database to match EF Core’s internal data model.
//The list of applied migrations is stored in the __EFMigrationsHistory table.


/*
 
 - Scaffolding of columns—EF Core uses conservative
values for things like string columns by allowing
strings of large or unlimited length. In practice,
you may want to restrict these and other data
types to sensible values.

- Validation—You can decorate your entities with
DataAnnotations validation attributes, but EF
Core won’t validate the values automatically before
saving to the database. This behavior differs from
EF 6.x behavior, in which validation was automatic.

- Handling concurrency—EF Core provides a few
ways to handle concurrency, which occurs when
multiple users attempt to update an entity at the
same time. One partial solution is to use
Timestamp columns on your entities.


- Handling errors—Databases and networks are
inherently flaky, so you’ll always have to account
for transient errors. EF Core includes various
features to maintain connection resiliency by
retrying on network failures.

- Synchronous vs. asynchronous—EF Core provides
both synchronous and asynchronous commands
for interacting with the database. Often, async is
better for web apps, but this argument has
nuances that make it impossible to recommend
one approach over the other in all situations.
 
 */

/*
 
 Most web applications use
some sort of database, so the following problems are likely
to affect ASP.NET Core developers at some point:

- Automatic migrations—If you deploy your app to
production automatically as part of some sort of
DevOps pipeline, you’ll inevitably need some way
to apply migrations to a database automatically.
You can tackle this situation in several ways, such
as scripting the .NET tool, applying migrations in
your app’s startup code, using EF Core bundles, or
using a custom tool. Each approach has its pros
and cons.

- Multiple web hosts—One specific consideration is
whether you have multiple web servers hosting
your app, all pointing to the same database. If so,
applying migrations in your app’s startup code
becomes harder, as you must ensure that only one
app can migrate the database at a time.

- Making backward-compatible schema changes—A
corollary of the multiple-web-host approach is that
you’ll often be in a situation in which your app
accesses a database that has a newer schema
than the app thinks. Normally, you should
endeavor to make schema changes backwardcompatible
wherever possible.

- Storing migrations in a different assembly—In this
chapter I included all my logic in a single project,
but in larger apps, data access is often in a
different project from the web app. For apps with
this structure, you must use slightly different
commands when using .NET CLI or PowerShell
cmdlets.

- Seeding data—When you first create a database,
you often want it to have some initial seed data,
such as a default user. EF 6.x had a mechanism for
seeding data built in, whereas EF Core requires
you to seed your database explicitly yourself.

 */