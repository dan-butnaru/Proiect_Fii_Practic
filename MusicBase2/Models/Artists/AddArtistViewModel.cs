using MusicBase.Entities;
using System.ComponentModel.DataAnnotations;

namespace MusicBase.Web.Models.Artists
{
    public class AddArtistViewModel
    {
        [MaxLength(50, ErrorMessage = "The artist name length is invalid!")]
        public string Name { get; set; }

        public Genre Genre { get; set; }

        /*
        public string mygenre { get; set; }
        public Genre GenreFromString(string genreString)
        {
            Genre genre;
            if (Enum.TryParse(genreString, true, out genre))
            {
                return genre;
            }
            else
            {
                throw new ArgumentException("Invalid genre: " + genreString);
            }
        }
        */

    }
}
