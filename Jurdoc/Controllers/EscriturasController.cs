using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jurdoc.Interface;
using Jurdoc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jurdoc.Controllers
{
    public class EscriturasController : Controller
    {
        IEscrituraService EscrituraService;
        public EscriturasController(IEscrituraService _EscrituraService)
        {
            EscrituraService = _EscrituraService;
        }
        public ActionResult Index()
        {
            IEnumerable<Escritura> Escritura = EscrituraService.GetEscrituras();
            return View(Escritura);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Escritura Escritura)
        {
            EscrituraService.AddEscritura(Escritura);
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Edit(int id)
        {
            Escritura Escritura = EscrituraService.GetEscritura(id);
            return View(Escritura);
        }
        [HttpPost]
        public ActionResult Edit(Escritura Escritura)
        {
            EscrituraService.EditEscritura(Escritura);
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Delete(int id)
        {
            Escritura Escritura = EscrituraService.GetEscritura(id);
            return View(Escritura);
        }
        [HttpPost]
        public ActionResult Delete(Escritura Escritura)
        {
            EscrituraService.DeleteEscritura(Escritura);
            return RedirectToAction(nameof(Index));
        }
    }
}
