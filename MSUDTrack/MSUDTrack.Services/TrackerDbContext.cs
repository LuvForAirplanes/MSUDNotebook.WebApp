using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MSUDTrack.DataModels.Models;
using System;
using System.ComponentModel;

namespace MSUDTrack.Services
{
    public partial class TrackerDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
        [DisplayName("Authorized Users")]
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public virtual DbSet<Child> Children { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Period> Period { get; set; }
        public virtual DbSet<Record> Records { get; set; }

        public TrackerDbContext(DbContextOptions<TrackerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Child>(entity =>
            {
                entity.ToTable("children");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Updated).HasColumnName("updated");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.IsActive).HasColumnName("is_active");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("foods");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Updated).HasColumnName("updated");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.ProteinGrams).HasColumnName("protein_grams");

                entity.Property(e => e.LeucineMilligrams).HasColumnName("leucine_milligrams");
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.ToTable("periods");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Updated).HasColumnName("updated");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.PeriodStart).HasColumnName("period_start");

                entity.Property(e => e.PeriodEnd).HasColumnName("period_end");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.ToTable("records");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Updated).HasColumnName("updated");

                entity.HasOne(p => p.Child)
                    .WithMany()
                    .HasForeignKey(d => d.ChildId)
                    .HasConstraintName("records_childid_fkey");

                entity.HasOne(p => p.Food)
                    .WithMany()
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("records_foodid_fkey");

                entity.HasOne(p => p.Period)
                    .WithMany()
                    .HasForeignKey(d => d.PeriodId)
                    .HasConstraintName("records_periodid_fkey");
            });
        }
    }
}
