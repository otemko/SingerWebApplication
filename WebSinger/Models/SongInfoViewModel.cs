using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelSinger;

namespace WebSinger.Models
{
    public class SongInfoViewModel
    {
        public Song CurrentSong { get; set; }
        public int CurrentId { get; set; }
    }
}