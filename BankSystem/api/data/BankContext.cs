using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {

    }

    public DbSet<Conta> Contas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Conta>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NumeroConta).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Saldo).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.Tipo).IsRequired();
            entity.Property(e => e.DataCriacao).IsRequired();
            entity.Property(e => e.Status).IsRequired();

            entity.HasOne(c => c.Cliente)
                  .WithMany(a => a.Contas)
                  .HasForeignKey("ClienteId")
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Cliente>(cliente =>
        {
            cliente.HasKey(c => c.Id);
            cliente.Property(c => c.Nome).IsRequired().HasMaxLength(100);
            cliente.Property(c => c.Cpf).IsRequired().HasMaxLength(11);
        });
    }
}