using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBase.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public int PopularSongId { get; set; }
        public IList<Song> Songs { get; set; }
    }
}
