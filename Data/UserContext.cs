using Microsoft.EntityFrameworkCore;
using webUserLoginTest.Models;

namespace webUserLoginTest.Data;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source=user.db");
    }
}//https://qiita.com/Nossa/items/9e552c972d215ea5e46b