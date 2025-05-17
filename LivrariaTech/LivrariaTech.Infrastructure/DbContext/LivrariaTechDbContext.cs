
using Microsoft.EntityFrameworkCore;
using LivrariaTech.Domain.Entities;

namespace LivrariaTech.Infrastructure;


public class LivrariaTechDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    
    public DbSet<Checkout> Checkouts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=/home/theghhz/Área de trabalho/Estudos/LivrariaTech/LivrariaTech.Data/TechLibraryDb.db");
    }
}
