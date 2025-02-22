using ETL.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETL.Persistence.Contexts;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
}