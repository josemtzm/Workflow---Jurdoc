using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jurdoc.Escrituras.Web.Models
{
    public class Escritura
    {
        public int IdEscritura { get; set; }

        public string NumeroEscritura { get; set; }

        public string Solicitante { get; set; }

        public DateTime FechaEscritura { get; set; }

        public string Observaciones { get; set; }

        public int Id_Tipo_Documento { get; set; }

        public int Id_Estatus { get; set; }

        public string Email { get; set; }
    }
}
