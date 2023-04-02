using MusicBase.Entities;
using MusicBase.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBase.Repositories.Artists
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly MusicContext _context;

        public ArtistRepository(MusicContext context)
        {
            _context = context;
        }
        public void AddArtist(ArtistDto artistDto)
        {
            if (artistDto == null) throw new ArgumentNullException(nameof(artistDto));
            if (string.IsNullOrEmpty(artistDto.Name)) throw new ArgumentException($"{nameof(artistDto.Name)} cannot be null or empty.");
            if (_context.Artists.Any(a => a.Name == artistDto.Name))
            {
                throw new Exception("Cannot add a new Artist with the same name.");
            }

            var artistEntity = new Artist
            {
               Name = artistDto.Name,
               Genre = artistDto.Genre,
               PopularSongId = artistDto.PopularSongId,
            };

            _context.Artists.Add(artistEntity);
        }

        public List<ArtistDto> GetAll()
        {
            return _context.Artists
                .Select(a => new ArtistDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Genre = a.Genre,
                    PopularSongId = a.PopularSongId,
                })
                .ToList();
        }

        public ArtistDto? GetArtist(string artistName)
        {
            if (artistName == null) throw new ArgumentOutOfRangeException(artistName);

            var artist = _context.Artists.SingleOrDefault(a => a.Name == artistName);

            if (artist == null) return null;

            var artistDto = new ArtistDto
            {
                Id = artist.Id,
                Name = artistName,
                Genre = artist.Genre,
                PopularSongId = artist.PopularSongId,
            };

            return artistDto;
        }

        public ArtistDto? GetArtistById(int artistId)
        {
            if (artistId == 0) throw new ArgumentOutOfRangeException(nameof(artistId));

            var artist = _context.Artists.SingleOrDefault(a => a.Id == artistId);

            if (artist == null) return null;

            var artistDto = new ArtistDto
            {
                Id = artistId,
                Name = artist.Name,
                Genre = artist.Genre,
                PopularSongId = artist.PopularSongId,
            };

            return artistDto;
        }

        public int GetArtistId(string artistName)
        {
            if (artistName == null) throw new ArgumentOutOfRangeException(artistName);

            var artist = _context.Artists.SingleOrDefault(a => a.Name == artistName);

            if(artist == null) return 0;
            else return artist.Id;
        }

        public string GetArtistName(int artistId)
        {
            if (artistId < 0) throw new ArgumentOutOfRangeException(nameof(artistId));

            var artist = _context.Artists.SingleOrDefault(a => a.Id == artistId);

            if (artist == null) return null;


            return artist.Name;
        }

        public void RemoveArtist(int artistId)
        {
            if (artistId <= 0) throw new ArgumentOutOfRangeException(nameof(artistId));

            var artistToDelete = _context.Artists.Find(artistId);

            if (artistToDelete != null)
            {
                _context.Artists.Remove(artistToDelete);
            }
        }

        public IEnumerable<ArtistDto> SearchByName(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public void UpdateArtist(ArtistDto artistDto)
        {
            if (artistDto == null) throw new ArgumentNullException(nameof(artistDto));
            if (string.IsNullOrEmpty(artistDto.Name)) throw new ArgumentException($"{nameof(artistDto.Name)} cannot be null or empty.");

            var artistToUpdate = _context.Artists.Find(artistDto.Id);
            if (artistToUpdate == null)
            {
                throw new Exception("The artist has not been found");
            }

            artistToUpdate.Name = artistDto.Name;
            artistToUpdate.Genre = artistDto.Genre;
        }
    }
}
