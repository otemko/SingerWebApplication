using ModelSinger;
using System.Data.Entity;

namespace ORMSinger
{
    public class SingerDbContext: DbContext
    {
        public SingerDbContext(): base("SingerDB")
        { }

        public DbSet<Singer> Singers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Accord> Accords { get; set; }
    }
}
