using Microsoft.EntityFrameworkCore;

namespace PrimerCasoEstudio_JuegoSnake.Models
{
    class DemoContext : DbContext
    {
        public DemoContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=LAPTOP-WAYNER;Database=db_Snake_Game;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }



        public DbSet<User> Users { get; set; }
    }
}


