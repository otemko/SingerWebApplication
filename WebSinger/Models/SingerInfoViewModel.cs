using System.Collections.Generic;
using ModelSinger;

namespace WebSinger.Models
{
    public class SingerInfoViewModel
    {
        public Singer Singer { get; set; }
        public IEnumerable<Song> Songs { get; set; }
        public IEnumerable<int> SongIds { get; set; }
        public PageInfo PageInfo { get; set; }
        public bool IsDesc { get; set; }
        public string SortName { get; set; }
    }
}