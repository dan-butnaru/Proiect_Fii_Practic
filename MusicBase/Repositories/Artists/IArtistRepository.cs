using MusicBase.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBase.Repositories.Artists
{
    public interface IArtistRepository
    {
        void AddArtist(ArtistDto artistDto);
        IEnumerable<ArtistDto> SearchByName(string searchTerm);
        void RemoveArtist(int artistId);
        List<ArtistDto> GetAll();
        ArtistDto? GetArtistById(int artistId);
        int GetArtistId(string artistName);
        string GetArtistName(int artistId);
        void UpdateArtist(ArtistDto artistDto);
        ArtistDto? GetArtist(string artistName);
    }
}
