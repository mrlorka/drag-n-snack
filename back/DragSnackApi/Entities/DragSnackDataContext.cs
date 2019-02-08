using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DragSnackApi.Entities
{
    public partial class DragSnackDataContext : DbContext
    {
        public DragSnackDataContext()
        {
        }

        public DragSnackDataContext(DbContextOptions<DragSnackDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectMember> ProjectMember { get; set; }
        public virtual DbSet<Teammember> Teammember { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=dragsnack.database.windows.net;Database=DragSnackData;User Id=dragger;Password=snagThi$");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ProjectMember>(entity =>
            {
                entity.ToTable("Project_Member");

                entity.HasIndex(e => new { e.ProjectId, e.MemberId })
                    .HasName("AK_Project_Member")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MemberId)
                    .IsRequired()
                    .HasColumnName("MemberID")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectId)
                    .IsRequired()
                    .HasColumnName("ProjectID")
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ProjectMember)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Member_Teammember");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectMember)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Member_Project");
            });

            modelBuilder.Entity<Teammember>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
