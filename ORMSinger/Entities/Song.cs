using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMSinger.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int Views { get; set; }

        public int? SingerId { get; set; }
        public virtual Singer Singer { get; set; }

        public virtual ICollection<Accord> Accords { get; set; }
    }
}
