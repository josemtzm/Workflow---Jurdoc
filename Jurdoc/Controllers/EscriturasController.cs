using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jurdoc.Api.Interface;
using Jurdoc.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jurdoc.Api.Controllers
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
        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            EscrituraService.DeleteEscritura(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
