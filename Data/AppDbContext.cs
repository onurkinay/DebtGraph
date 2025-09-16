using Microsoft.EntityFrameworkCore;

namespace UGTest.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}