using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMSinger.Entities
{
    public class Singer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public int Views { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
