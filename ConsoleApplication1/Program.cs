using System;
using System.Linq;
using System.Threading.Tasks;
using ParseSinger;
using BLLSinger.Services;
using DALSinger;
using ORMSinger;

namespace ConsoleApplication1
{
    class Program
    {
        static  void Main(string[] args)
        {
            NewMethod();
        }

        private static void NewMethod()
        {
            //var singers = Parse.GetSingers("http://amdm.ru/chords/");
            var singers = Parse.GetSingersFromPage("http://amdm.ru/chords/");            
            var service = new SingerService(new Repository(new SingerDbContext()));
            service.CreateRange(singers.ToArray());
        }
    }
}
