using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelSinger;

namespace WebSinger.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Singer> Singers { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}