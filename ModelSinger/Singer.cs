using System.Collections.Generic;

namespace ModelSinger
{
    public class Singer: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public int Views { get; set; }
        public string Url { get; set; }
        public int CountSong { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
