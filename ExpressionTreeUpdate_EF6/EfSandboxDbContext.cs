using ExpressionTreeUpdate_EF6.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpressionTreeUpdate_EF6
{
    public partial class EfSandboxDbContext : DbContext
    {
        public EfSandboxDbContext()
        {
        }

        public EfSandboxDbContext(DbContextOptions<EfSandboxDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=BLUEBOX\\SQLEXPRESS;Database=EfSandboxDb;Trusted_Connection=True;Encrypt=False");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
