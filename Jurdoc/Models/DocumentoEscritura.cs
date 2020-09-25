using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jurdoc.Api.Models
{
    public class DocumentoEscritura
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdEscritura { get; set; }
    }
}
