using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLLSinger.Interfaces;

namespace WebSinger.Controllers
{
    public class HomeController : Controller
    {
        private ISingerService singerService;
        private ISongService songService;

        public HomeController(ISingerService singerService, ISongService songService)
        {
            this.singerService = singerService;
            this.songService = songService;
        }

        public async Task<ActionResult> Index()
        {
            var singers = await singerService.GetAllAsync();
            return View(singers);
        }

        public async Task<ActionResult> SingerInfo(int id)
        {
            var singer = await singerService.Get(id);
            return View(singer);
        }

        public async Task<ActionResult> SongInfo(int id)
        {
            var song = await songService.Get(id);
            return View(song);
        }
    }
}