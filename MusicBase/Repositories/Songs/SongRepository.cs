using MusicBase.Entities;
using MusicBase.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBase.Repositories.Songs
{
    public class SongRepository : ISongRepository
    {
        private readonly MusicContext _context;

        public SongRepository(MusicContext context)
        {
            _context = context;
        }
        public void AddSong(SongDto songDto)
        {
            if (songDto == null) throw new ArgumentNullException(nameof(songDto));
            if (string.IsNullOrEmpty(songDto.Name)) throw new ArgumentException($"{nameof(songDto.Name)} cannot be null or empty.");
            if (_context.Songs.Any(s => s.Name == songDto.Name))
            {
                throw new Exception("Cannot add song. It's already added in our database");
            }

            var songEntity = new Song
            {
                Name = songDto.Name,
                ArtistId = songDto.ArtistId,
                NrLikes = songDto.NrLikes,
                NrPlays = songDto.NrPlays,
            };

            _context.Songs.Add(songEntity);
        }

        public void DeleteSong(int songId)
        {
            if (songId <= 0) throw new ArgumentOutOfRangeException(nameof(songId));

            var songToDelete = _context.Songs.Find(songId);

            if (songToDelete != null)
            {
                _context.Songs.Remove(songToDelete);
            }
        }

        public List<SongDto> GetAll()
        {
            return _context.Songs
                .Select(s => new SongDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    ArtistId = s.ArtistId,
                    NrPlays = s.NrPlays,
                    NrLikes = s.NrLikes,
                })
                .ToList();
        }

        public SongDto? GetSong(int songId)
        {
            if (songId <= 0) throw new ArgumentOutOfRangeException(nameof(songId));

            var song = _context.Songs.SingleOrDefault(s => s.Id == songId);

            if (song == null) return null;

            var songDto = new SongDto
            {
                Id= songId,
                Name = song.Name,
                ArtistId = song.ArtistId,
                NrPlays = song.NrPlays,
                NrLikes = song.NrLikes,
            };

            return songDto;
        }

        public IEnumerable<SongDto> SearchByName(string searchTerm)
        {
            return _context.Songs
               .Where(s => s.Name.Contains(searchTerm))
               .Select(s => new SongDto
               {
                   Id= s.Id,
                   Name = s.Name,
                   ArtistId = s.ArtistId,
                   NrLikes = s.NrLikes,
                   NrPlays = s.NrPlays,
               })
               .ToList();
        }

        public void UpdateSong(SongDto songDto)
        {
            if (songDto == null) throw new ArgumentNullException(nameof(songDto));
            if (string.IsNullOrEmpty(songDto.Name)) throw new ArgumentException($"{nameof(songDto.Name)} cannot be null or empty.");

            var songToUpdate = _context.Songs.Find(songDto.Id);
            if (songToUpdate == null)
            {
                throw new Exception("The user has not been found");
            }

            songToUpdate.Name = songDto.Name;
            songToUpdate.ArtistId = songDto.ArtistId;

        }

    }
}

