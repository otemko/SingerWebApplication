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
            var accordsName = songView.AccordStrings[0] == "" ? null : songView.AccordStrings[0].Split(',');

            var accords = new List<Accord>();

            if (accordsName != null)
            {
                accords = (await accordService.GetAccordsByAccordNames(accordsName)).ToList();
            }
            else
            {
                accords = null;
            }

            songService.Update(new Song
            {
                Id = songView.Id,
                Accords = accords,
                Name =  songView.Name,
                Text = songView.Text
            });

            return RedirectToAction("EditSong", new {id = songView.Id});
        }

        public async Task<ActionResult> AutocompleteSearch(string term)
        {
            var accords = await accordService.GetAllAsync();
            var accordNames = accords.Select(x => x.Name);

            return Json(accordNames, JsonRequestBehavior.AllowGet);
        }
    }
}