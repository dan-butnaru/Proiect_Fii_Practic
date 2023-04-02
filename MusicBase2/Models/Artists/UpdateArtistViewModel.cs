using MusicBase.Entities;
using System.ComponentModel.DataAnnotations;

namespace MusicBase.Web.Models.Artists
{
    public class UpdateArtistViewModel
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The Song name length is invalid!")]
        public string Name { get; set; }
        public Genre Genre { get; set; }
    }
}
