using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnicamAppelli.Modello;
using UnicamAppelli.Modello.Richieste;
using UnicamAppelli.Servizi;

namespace UnicamAppelli.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServizioCorsi servizioCorsi;
        public HomeController(IServizioCorsi servizioCorsi)
        {
            this.servizioCorsi = servizioCorsi;
        }
        public async Task<IActionResult> Index()
        {
            //List<Corso> listaCorsi = await db.Corsi.Include(corso => corso.Appelli).ToListAsync();
            IEnumerable<Corso> listaCorsi = await servizioCorsi.Elenca();
            return View(listaCorsi);
        }

        [HttpGet]
        public IActionResult Nuovo(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuovo(CreazioneCorso  parametro) {

            if (ModelState.IsValid) {
                Corso nuovo = new Corso(parametro.Nome, parametro.Docente);
                await servizioCorsi.Crea(nuovo);
                return RedirectToAction("Index");
            } else {
                return View(parametro);
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
