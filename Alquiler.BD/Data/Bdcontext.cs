using Alquiler.BD.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Alquiler.BD.Data
{
    public class Bdcontext : DbContext
    {
        public Bdcontext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Persona> Personas { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Direccion> Direcciones { get; set; }



        public DbSet<TipoDocumento> TipoDocumentos { get; set; }



        public DbSet<Foto> Fotos { get; set; }

    }
}