using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jurdoc.Api.Interface;
using Jurdoc.Api.Models;
using Jurdoc.Api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jurdoc.Api.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoEscrituraController : ControllerBase
    {
        IDocumentoEscrituraService documentoEscrituraService;
        public DocumentoEscrituraController(IDocumentoEscrituraService _documentoEscrituraService)
        {
            documentoEscrituraService = _documentoEscrituraService;
        }
        // GET: api/<EscriturasController>
        [HttpGet]
        public IEnumerable<DocumentoEscritura> Get()
        {
            return documentoEscrituraService.GetDocEscrituras();
        }

        // GET api/<EscriturasController>/5
        [HttpGet("{id}")]
        public DocumentoEscritura Get(int id)
        {
            return documentoEscrituraService.GetDocEscritura(id);
        }

        // POST api/<EscriturasController>
        [HttpPost]
        public void Post(DocumentoEscritura DocumentoEscritura)
        {
            documentoEscrituraService.AddDocEscritura(DocumentoEscritura);
        }

        // PUT api/<EscriturasController>/5
        [HttpPut("{id}")]
        public void Put(int id, DocumentoEscritura DocumentoEscritura)
        {
            if (id != 0)
                documentoEscrituraService.EditDocEscritura(DocumentoEscritura);

        }

        // DELETE api/<EscriturasController>/5
        [HttpDelete]
        public void Delete(int id)
        {
            DocumentoEscritura DocumentoEscritura = documentoEscrituraService.GetDocEscritura(id);
            Delete(DocumentoEscritura);
        }
        public void Delete(DocumentoEscritura DocumentoEscritura)
        { 
            documentoEscrituraService.DeleteDocEscritura(DocumentoEscritura);
        }
    }
}
