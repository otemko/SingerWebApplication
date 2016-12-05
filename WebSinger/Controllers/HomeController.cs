using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLLSinger.Interfaces;
using ModelSinger;
using WebSinger.Models;

namespace WebSinger.Controllers
{
    public class HomeController : Controller
    {
        private ISingerService singerService;
        private ISongService songService;
        private IAccordService accordService;

        public HomeController(ISingerService singerService, ISongService songService, IAccordService accordService)
        {
            this.singerService = singerService;
            this.songService = songService;
            this.accordService = accordService;
        }

        public async Task<ActionResult> Index(int page = 1, bool isDesc = true, string name = "Name")
        {
            var pageSize = 2;
            
            var singers = await singerService.GetPartOrderBy(pageSize, (page - 1) * pageSize, isDesc, name);
            var count = singerService.GetCount();
            var pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = count};
            var ivm = new IndexViewModel { PageInfo = pageInfo, Singers = singers, SortName = name, IsDesc = isDesc};
            return View(ivm);
        }

        public async Task<ActionResult> SingerInfo(int id, int page = 1, bool isDesc = true, string name = "Name")
        {
            var pageSize = 10;

            var singer = await singerService.Get(id);
            var songs = await songService.GetBySingerIdPartOrderBy(id, pageSize, (page - 1)*pageSize, isDesc, name);
            var songIds = songs.Select(s => s.Id);
            var countSong = songService.GetCountBySungerId(id);
            var pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = countSong };
            var sivm = new SingerInfoViewModel() { PageInfo = pageInfo, IsDesc = isDesc, Singer = singer, Songs = songs, SortName = name, SongIds = songIds};

            return View(sivm);
        }

        public async Task<ActionResult> SongInfo(int id)
        {
            var song = await songService.Get(id);
            TempData["CurrentId"] = id;
            return View(song);
        }

        [HttpGet]
        public async Task<ActionResult> EditSong(int id)
        {
            var song = await songService.Get(id);
            var accordNames = song.Accords.Select(a => a.Name).ToArray();
            return View(new SongViewModel
            {
                Id = song.Id,
                Name =  song.Name,
                AccordStrings = accordNames,
                Text = song.Text
            });
        }

        [HttpPost]
        public async Task<ActionResult> EditSong(SongViewModel songView)
        {
            var accordsName = songView.AccordStrings[0] == "" ? null : 
                songView.AccordStrings[0].Split(',').Select(a => a.Trim()).ToArray();
            
            List<Accord> accords;

            accords = accordsName != null ? (await accordService.GetAccordsByAccordNames(accordsName)).ToList() : null;

            var song = await songService.Get(songView.Id);
            song.Accords = accords;
            song.Text = songView.Text;

            songService.Update(song);

            return RedirectToAction("SingerInfo", new {id = song.SingerId});
        }

        public async Task<ActionResult> AutocompleteSearch()
        {
            var accords = await accordService.GetAllAsync();
            var accordNames = accords.Select(x => x.Name);

            return Json(accordNames, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetNextOrPrevSong(int currentSingerId, string buttonType)
        {
            var songs = await songService.GetBySingerId(currentSingerId);
            int index = 0;

            var currentId = Convert.ToInt32(TempData["CurrentId"]); ;

            for (int i = 0; i < songs.Count(); i++)
            {
                if (songs.ElementAt(i).Id == currentId)
                {
                    if (buttonType == "next")
                    {
                        index = i + 1;
                        index = index >= songs.Count() ? 0 : index;
                    }
                    if (buttonType == "prev")
                    {
                        index = i - 1;
                        index = index < 0 ? songs.Count() - 1  : index;
                    }
                    break;
                }
            }

            var newSong = songs.ElementAt(index);
            
            TempData["CurrentId"] = newSong.Id;

            return PartialView("_PartialSong", newSong);
        }
    }
}