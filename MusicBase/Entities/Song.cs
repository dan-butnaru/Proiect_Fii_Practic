using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBase.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ArtistId { get; set; }
        public int NrPlays { get; set; }
        public int NrLikes { get; set; }
        public IList<User> Users { get; set; }
    }
}
