using Microsoft.EntityFrameworkCore;
using MusicBase.Entities;
using Microsoft.Extensions.Logging;

namespace MusicBase
{
    public class MusicContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<UserSongs> UserSongs { get; set; }
        public DbSet<ArtistSongs> ArtistsSongs { get; set; }

        public MusicContext(DbContextOptions options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArtistSongs>()
              .HasKey(sa => new { sa.ArtistId, sa.SongId });

            modelBuilder.Entity<UserSongs>()
              .HasKey(us => new { us.UserId, us.SongId });

            modelBuilder.Entity<Artist>()
              .HasMany(u => u.Songs);

            modelBuilder.Entity<User>()
              .HasMany(u => u.Songs)
              .WithMany(s => s.Users)
              .UsingEntity<UserSongs>();
        }
    }

}
