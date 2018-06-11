using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1.Models
{
    public partial class psr03951DataBaseContext : DbContext
    {
        public psr03951DataBaseContext()
        {
        }

        public psr03951DataBaseContext(DbContextOptions<psr03951DataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Rejoint> Rejoint { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-3Q1QMSK;Database=psr03951DataBase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DeactivatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsInactive).HasColumnName("isInactive");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Rejoint>(entity =>
            {
                entity.HasKey(e => new { e.IdGroup, e.IdUser });

                entity.ToTable("rejoint");

                entity.Property(e => e.IdGroup).HasColumnName("idGroup");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationDate).HasColumnType("date");

                entity.Property(e => e.DeactiveDate).HasColumnType("datetime");

                entity.Property(e => e.EmailAdress).IsRequired();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GravatarUrl).HasMaxLength(10);

                entity.Property(e => e.IdGroup).HasColumnName("idGroup");

                entity.Property(e => e.IsInactive).HasColumnName("isInactive");

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("FK_User_Group");
            });
        }
    }
}
