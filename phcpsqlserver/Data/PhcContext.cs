using Microsoft.EntityFrameworkCore;
using System;

namespace Phc.Data
{
    public sealed class PhcContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public PhcContext(DbContextOptions<PhcContext> options)
            : base(options)
        {
            //throw new InvalidOperationException("ayo");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        /*
        void SetBandTypes(ModelBuilder modelBuilder){
          modelBuilder.Entity<Band>(
              entitybuilder => {
                  entitybuilder.Property(b => b.Id).HasColumnType("");
                  entitybuilder.Property(b => b.Rating).HasColumnType("decimal(5, 2)");
              }
          );
        }
        */

        void SetBandColumnNames(ModelBuilder modelBuilder)
        {
            // configure relation band

            modelBuilder.Entity<Band>()
              .Property(b => b.Id)
              .HasColumnName("band_id");

            modelBuilder.Entity<Band>()
              .Property(b => b.Name)
              .HasColumnName("name");

            modelBuilder.Entity<Band>()
              .Property(b => b.Formed)
              .HasColumnName("formed");

            modelBuilder.Entity<Band>()
              .Property(b => b.AddedOn)
              .HasColumnName("added_on");

            modelBuilder.Entity<Band>()
              .Property(b => b.LastModified)
              .HasColumnName("last_modified");

            modelBuilder.Entity<Band>()
              .ToTable("bands");

            // configure relation albums

            modelBuilder.Entity<Album>()
              .Property(a => a.Id)
              .HasColumnName("album_id");

            modelBuilder.Entity<Album>()
              .Property(a => a.Name)
              .HasColumnName("name");

            modelBuilder.Entity<Album>()
              .Property(a => a.Runtime)
              .HasColumnName("formed");

            modelBuilder.Entity<Album>()
              .ToTable("albums");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetBandColumnNames(modelBuilder);
            //SetBandTypes(modelBuilder);
        }
    }
}
