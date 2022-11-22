using Microsoft.EntityFrameworkCore;
using Shared.Model;

namespace EfcDataAccess;

public class PostContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/David.db");
    }
}