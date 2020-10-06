using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jurdoc.Api.Interface;
using Jurdoc.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jurdoc.Api.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscriturasController : ControllerBase
    {
        IEscrituraService EscrituraService;
        public EscriturasController(IEscrituraService _EscrituraService)
        {
            EscrituraService = _EscrituraService;
        }
        // GET: api/<EscriturasController>
        [HttpGet]
        public IEnumerable<Escritura> Get()
        {
            return EscrituraService.ReadData<Escritura>();
        }

        // GET api/<EscriturasController>/5
        [HttpGet("{id}")]
        public Escritura Get(int id)
        {
            return EscrituraService.GetEscritura(id);
        }

        // POST api/<EscriturasController>
        [HttpPost]
        public void Post(Escritura escritura)
        {
            EscrituraService.AddEscritura(escritura);
        }

        // PUT api/<EscriturasController>/5
        [HttpPut("{id}")]
        public void Put(int id, Escritura escritura)
        {
            if (id != 0)
                EscrituraService.EditEscritura(escritura);

        }

        // PUT api/<EscriturasController>/5
        [HttpPut]
        public void Put(Escritura escritura)
        {
            EscrituraService.EditEscritura(escritura);
        }

        // DELETE api/<EscriturasController>/5
        [HttpDelete]
        public void Delete(int id)
        {
            EscrituraService.DeleteEscritura(id);
        }
    }
}
