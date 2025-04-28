using Microsoft.EntityFrameworkCore;
using Parcial1_Programacion3_28_4.Models;


namespace Parcial1_Programacion3_28_4.Data
{
    public class AppDBcontext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=TOJESHO\\SQLEXPRESS;Initial Catalog=parcial1Programacion3;Integrated Security=True; Trust Server Certificate=true");
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<ObraSocial> ObraSociales { get; set; }


    }
}
