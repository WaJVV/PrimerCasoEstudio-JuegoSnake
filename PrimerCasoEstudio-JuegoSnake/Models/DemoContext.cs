using Microsoft.EntityFrameworkCore;

namespace PrimerCasoEstudio_JuegoSnake.Models
{
    class DemoContext : DbContext
    {
        string DBJohnny = "Server=DESKTOP-28F731Q\\SQLEXPRESS;Database=db_Snake_Game;Trusted_Connection=True;TrustServerCertificate=True;";
        string DBWayner = "Server=LAPTOP-WAYNER;Database=db_Snake_Game;Trusted_Connection=True;TrustServerCertificate=True;";
        public DemoContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@DBJohnny);
            }
        }



        public DbSet<User> Users { get; set; }
    }
}


