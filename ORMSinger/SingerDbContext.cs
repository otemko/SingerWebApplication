using ORMSinger.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
