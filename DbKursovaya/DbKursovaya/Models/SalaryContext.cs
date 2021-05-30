using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DbKursovaya.Models
{
    public partial class SalaryContext : DbContext
    {
        public SalaryContext()
        {
        }

        public SalaryContext(DbContextOptions<SalaryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeClient> EmployeeClients { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Salary;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SalaryFixed).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK_Сотрудник_Организация");
            });

            modelBuilder.Entity<EmployeeClient>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.ClientId })
                    .HasName("PK_Сотрудник_Клиент");

                entity.ToTable("EmployeeClient");

                entity.Property(e => e.AttractingDate).HasColumnType("datetime");

                entity.Property(e => e.DealDate).HasColumnType("datetime");

                entity.Property(e => e.DealSum).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.EmployeeClients)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_EmployeeClient_Client");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeClients)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_EmployeeClient_Employee1");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("Organization");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.ToTable("Salary");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.Sum).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Зарплата_Сотрудник");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
