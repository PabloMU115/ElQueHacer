using Microsoft.EntityFrameworkCore;
using ElQueHacer.Models;

namespace ElQueHacer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>()
                .HasOne(p => p.Usuario)
                .WithMany(prov => prov.Tareas)
                .HasForeignKey(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict); // Evita eliminación en cascada, opcional
        }

        public DbSet<Usuarios> Usuario { get; set; }

        public DbSet<Tarea> Tarea { get; set; }
    }
}
