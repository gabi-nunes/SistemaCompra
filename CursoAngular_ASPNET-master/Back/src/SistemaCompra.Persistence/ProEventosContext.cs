using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options){}
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        protected override void OnModelCreating(ModelBuilder mb){
            mb.Entity<PalestranteEvento>().HasKey(PE => new {PE.EventoId, PE.PalestranteId});
             
            mb.Entity<Evento>().HasMany(e => e.RedesSociais)
                               .WithOne(rs => rs.Evento)
                               .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<Palestrante>().HasMany(p => p.RedesSociais)
                               .WithOne(rs => rs.Palestrante)
                               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}