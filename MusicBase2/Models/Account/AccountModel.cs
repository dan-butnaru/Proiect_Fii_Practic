using System.ComponentModel.DataAnnotations;

namespace MusicBase.Web.Models.Account
{
    public class AccountModel
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The name length is invalid!")]
        public string Name { get; set; }
        public string ArtistName { get; set; }
    }
}
