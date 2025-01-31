using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace exercise.wwwapi.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<BlogPost> Posts { get; set; }
    
    private readonly string _connectionString;

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<BlogPost>().HasKey(p => p.Id);
        
        modelBuilder.Entity<User>().HasMany(u => u.BlogPosts).WithOne(p => p.Author);
        
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                DisplayName = "John Doe",
                Username = "j.doe",
                Email = "john@gmail.com",
                Password = "password",
            }
        );
        
        modelBuilder.Entity<BlogPost>().HasData(
            new BlogPost
            {
                Id = 1,
                Title = "First Post",
                Content = "Hello World!",
                AuthorId = 1,
            }
        );
        
        base.OnModelCreating(modelBuilder);
    }
}