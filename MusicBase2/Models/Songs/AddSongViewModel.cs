using System.ComponentModel.DataAnnotations;
using MusicBase.Repositories.Artists;
namespace MusicBase.Web.Models.Songs
{
    public class AddSongViewModel
    {
        [MaxLength(50, ErrorMessage = "The song name length is invalid!")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "The artist name is invalid!")]
        public string ArtistName { get; set; }

    }
}
