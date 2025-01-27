using Microsoft.EntityFrameworkCore;
using TesteDotkon.Domain.Entities;

namespace TesteDotkon.Infra.Data.Context;

public class TesteDotkonContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Usuario> UsuarioDbSet { get; set; }
    public DbSet<Postagem> PostagemDbSet { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
