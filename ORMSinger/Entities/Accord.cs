using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMSinger.Entities
{
    public class Accord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
