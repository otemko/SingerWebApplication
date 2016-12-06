using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLLSinger.Interfaces;
using ModelSinger;
using MvcBreadCrumbs;
using WebSinger.Helpers;
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

        [BreadCrumb(Clear = true, Label = "Singers")]
        public async Task<ActionResult> Index(int page = 1, bool isDesc = true, string name = "Name")
        {
            var pageSize = 5;
            
            var skip = (page - 1)*pageSize;

            var singers = (IEnumerable<Singer>)CacheHelper.Get("singers" + page + pageSize + skip + isDesc + name);
            if (singers == null)
            {
                singers = await singerService.GetPartOrderBy(pageSize, skip, isDesc, name);
                CacheHelper.Set("singers" + page + pageSize + skip + isDesc + name, singers);
            }

            int count;
            if (CacheHelper.Get("singerCount") != null)
                count = (int)CacheHelper.Get("singerCount");
            else
            {
                count = singerService.GetCount(); 
                CacheHelper.Set("singerCount", count);
            }
            
            var pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = count};
            var ivm = new IndexViewModel { PageInfo = pageInfo, Singers = singers, SortName = name, IsDesc = isDesc};

            return View(ivm);
        }
        [BreadCrumb]
        public async Task<ActionResult> SingerInfo(int id, int page = 1, bool isDesc = true, string name = "Name")
        {
           
            var pageSize = 10;

            var singer = (Singer)CacheHelper.Get("singer" + id);
            if (singer == null)
            {
                singer = await singerService.Get(id);
                CacheHelper.Set("singer" + id, singer);
            }

            //readCrumb.Add(Url.Action("SongInfo", "Home", new { id, page, isDesc, name }), singer.Name);
            BreadCrumb.SetLabel(singer.Name);

            int skip = (page - 1)*pageSize;
            var songs = (IEnumerable<Song>)CacheHelper.Get("songs" + id + pageSize + skip + isDesc + name);
            if (songs == null)
            { 
                songs = await songService.GetBySingerIdPartOrderBy(id, pageSize, skip, isDesc, name);
                CacheHelper.Set("songs" + id + pageSize + skip + isDesc + name, songs);
            }
            
            int countSong;
            if (CacheHelper.Get("songsCount" + id) != null)
                countSong = (int)CacheHelper.Get("songsCount" + id);
            else
            {
                countSong = songService.GetCountBySingerId(id);
                CacheHelper.Set("songsCount" + id, countSong);
            }
            
            var pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = countSong };
            var sivm = new SingerInfoViewModel() { PageInfo = pageInfo, IsDesc = isDesc, Singer = singer, Songs = songs, SortName = name};

            return View(sivm);
        }

        [BreadCrumb]
        public async Task<ActionResult> SongInfo(int id)
        {
            var song = (Song)CacheHelper.Get("song" + id);
            
            if (song == null)
            {
                song = await songService.Get(id);
                CacheHelper.Set("song" + id, song);
            }
           
            BreadCrumb.SetLabel(song.Name);

            TempData["CurrentId"] = id;
            return View(song);
        }

        [HttpGet]
        public async Task<ActionResult> EditSong(int id)
        {
            var song = (Song)CacheHelper.Get("song" + id);
            if (song == null)
            {
                song = await songService.Get(id);
                CacheHelper.Set("song" + id, song);
            }

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

            var song = (Song)CacheHelper.Get("song" + songView.Id);
            if (song == null)
            {
                song = await songService.Get(songView.Id);
                CacheHelper.Set("song" + songView.Id, song);
            }

            song.Accords = accords;
            song.Text = songView.Text;

            songService.Update(song);
            CacheHelper.Change("song" + songView.Id, song);

            return RedirectToAction("SingerInfo", new {id = song.SingerId});
        }

        public async Task<ActionResult> AutocompleteSearch()
        {
            var accords = (IEnumerable<Accord>) CacheHelper.Get("allAccords");
            if (accords == null)
            {
                accords = await accordService.GetAllAsync(); ;
                CacheHelper.Set("allAccords", accords);
            }
            
            var accordNames = accords.Select(x => x.Name);

            return Json(accordNames, JsonRequestBehavior.AllowGet);
        }

        
        public async Task<ActionResult> GetNextOrPrevSong(int currentSingerId, string buttonType)
        {
            var songs = (IEnumerable<Song>)CacheHelper.Get("songsByUserId" + currentSingerId);
            if (songs == null)
            {
                songs = await songService.GetBySingerId(currentSingerId);
                CacheHelper.Set("songsByUserId" + currentSingerId, songs);
            }

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

            BreadCrumb.DeleteLast();
            BreadCrumb.Add(Url.Action("SongInfo", "Home", new { id = newSong.Id }), newSong.Name);

            TempData["CurrentId"] = newSong.Id;

            return PartialView("_PartialSong", newSong);
        }
    }
}