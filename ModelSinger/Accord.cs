using System.Collections.Generic;

namespace ModelSinger
{
    public class Accord: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
