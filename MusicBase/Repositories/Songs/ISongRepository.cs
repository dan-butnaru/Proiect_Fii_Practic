using MusicBase.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBase.Repositories.Songs
{
    public interface ISongRepository
    {
        void AddSong(SongDto songDto);
        IEnumerable<SongDto> SearchByName(string searchTerm);
        void DeleteSong(int songId);
        List<SongDto> GetAll();
        SongDto? GetSong(int songId);
        void UpdateSong(SongDto songDto);
    }
}
