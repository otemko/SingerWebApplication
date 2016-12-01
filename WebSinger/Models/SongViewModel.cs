using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSinger.Models
{
    public class SongViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] AccordStrings { get; set; }
        public string Text { get; set; }
    }
}