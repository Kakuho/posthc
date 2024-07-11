using Microsoft.EntityFrameworkCore;
using System;

namespace Phc.Data
{
    public sealed class PhcContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<JoinPlaylistAlbum> PlaylistsAlbum { get; set; }
        

        public PhcContext(DbContextOptions<PhcContext> options)
            : base(options)
        {
            //throw new InvalidOperationException("ayo");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        void MapBandColumns(ModelBuilder modelBuilder){
            modelBuilder.Entity<Band>()
              .Property(b => b.Id)
              .HasColumnName("band_id");

            modelBuilder.Entity<Band>()
              .Property(b => b.Name)
              .HasColumnName("name");
            
            modelBuilder.Entity<Band>()
              .Property(b => b.Genre)
              .HasColumnName("genre");

            modelBuilder.Entity<Band>()
              .Property(b => b.Formed)
              .HasColumnName("formed");

            modelBuilder.Entity<Band>()
              .Property(b => b.AddedOn)
              .HasColumnName("added_on");

            modelBuilder.Entity<Band>()
              .Property(b => b.LastUpdated)
              .HasColumnName("last_updated");

            modelBuilder.Entity<Band>()
              .ToTable("bands");
        }

        void MapAlbumColumns(ModelBuilder modelBuilder){
            modelBuilder.Entity<Album>()
              .Property(a => a.Id)
              .HasColumnName("album_id");

            modelBuilder.Entity<Album>()
              .Property(a => a.Name)
              .HasColumnName("name");

            modelBuilder.Entity<Album>()
              .Property(a => a.Runtime)
              .HasColumnName("runtime");

            modelBuilder.Entity<Album>()
              .Property(a => a.Genre)
              .HasColumnName("genre");

            modelBuilder.Entity<Album>()
              .Property(a => a.AddedOn)
              .HasColumnName("added_on");

            modelBuilder.Entity<Album>()
              .Property(a => a.LastUpdated)
              .HasColumnName("last_updated");

            modelBuilder.Entity<Album>()
              .Property(a => a.bandId)
              .HasColumnName("band_id");

            modelBuilder.Entity<Album>()
              .ToTable("albums");
        }

        void MapPlaylistColumns(ModelBuilder modelBuilder){
            modelBuilder.Entity<Playlist>()
              .Property(p => p.Id)
              .HasColumnName("playlist_id");

            modelBuilder.Entity<Playlist>()
              .Property(p => p.Name)
              .HasColumnName("name");

            modelBuilder.Entity<Playlist>()
              .Property(p => p.Runtime)
              .HasColumnName("runtime");

            modelBuilder.Entity<Playlist>()
              .Property(p => p.AddedOn)
              .HasColumnName("added_on");
            
            
            modelBuilder.Entity<Playlist>()
              .ToTable("playlist");
        }

        void ConfigurePlaylistAlbumJoin(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlist>()
              .HasMany(p => p.Albums)
              .WithMany(a => a.Playlists)
              .UsingEntity<JoinPlaylistAlbum>();

            modelBuilder.Entity<JoinPlaylistAlbum>()
              .Property(j => j.PlaylistId)
              .HasColumnName("playlistid");

            modelBuilder.Entity<JoinPlaylistAlbum>()
              .Property(j => j.AlbumId)
              .HasColumnName("albumid");

            modelBuilder.Entity<JoinPlaylistAlbum>()
              .ToTable("playlist_album");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapBandColumns(modelBuilder);
            MapAlbumColumns(modelBuilder);
            MapPlaylistColumns(modelBuilder);
            ConfigurePlaylistAlbumJoin(modelBuilder);
        }
    }
}
