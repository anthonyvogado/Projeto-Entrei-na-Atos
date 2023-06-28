using BackEndAPI.Models;
using Microsoft.EntityFrameworkCore;

public class Contexto : DbContext
{
    public DbSet<Medicamento> Medicamentos { get; set; }
    public DbSet<Classe> Classes { get; set; }
    public DbSet<Tipo> Tipos { get; set; }
    public DbSet<IndicadoTag> IndicadoTags { get; set; }
    public DbSet<ContraIndicadoTag> ContraIndicadoTags { get; set; }

    public Contexto()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        try
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=BancoV3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medicamento>()
            .HasOne(m => m.Classe)
            .WithMany(c => c.Medicamentos)
            .HasForeignKey(m => m.ClasseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Medicamento>()
            .HasOne(m => m.Tipo)
            .WithMany(t => t.Medicamentos)
            .HasForeignKey(m => m.TipoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Medicamento>()
            .HasMany(m => m.IndicadoTags)
            .WithMany(it => it.Medicamentos)
            .UsingEntity("MedicamentoIndicadoTag");

        modelBuilder.Entity<Medicamento>()
            .HasMany(m => m.ContraIndicadoTags)
            .WithMany(cit => cit.Medicamentos)
            .UsingEntity("MedicamentoContraIndicadoTag");
    }
}