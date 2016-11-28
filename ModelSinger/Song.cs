using System.Collections.Generic;

namespace ModelSinger
{
    public class Song: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int Views { get; set; }
        public string Url { get; set; }

        public int? SingerId { get; set; }
        public virtual Singer Singer { get; set; }

        public virtual ICollection<Accord> Accords { get; set; }
    }
}
