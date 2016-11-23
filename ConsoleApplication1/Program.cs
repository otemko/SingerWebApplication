using ORMSinger;
using ORMSinger.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SingerDbContext dc = new SingerDbContext())
            {
                Singer s = new Singer { Biography = "asd", Name = "sad", Views = 1 };
                dc.Singers.Add (s);
                dc.SaveChanges();

                var users = dc.Singers;
                Console.WriteLine("Список объектов:");
                foreach (Singer u in users)
                {
                    Console.WriteLine("{0}.{1} - {2}", u.Id, u.Name);
                }

            }
        }
    }
}
