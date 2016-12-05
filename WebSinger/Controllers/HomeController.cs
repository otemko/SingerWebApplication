﻿using System;
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
            var countSong = songService.GetCountBySungerId(id);
            var pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = countSong };
            var sivm = new SingerInfoViewModel() { PageInfo = pageInfo, IsDesc = isDesc, Singer = singer, Songs = songs, SortName = name};

            return View(sivm);
        }

        public async Task<ActionResult> SongInfo(int id)
        {
            var song = await songService.Get(id);
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
    }
}