using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jurdoc.Api.Models
{
    public class Escritura
    {
        public int IDESCRITURA { get; set; }
        [Required(ErrorMessage = "El campo \"Numero de Escritura\" es requerido, solo acepta números enteros y debe ser único.")]
        [Display(Name = "Número de Escritura:")]
        public string NUMEROESCRITURA { get; set; }

        [Display(Name = "Quien Solicita:")]
        public string SOLICITANTE { get; set; }

        //[Display(Name = "Fecha de Escritura:")]
        //public DateTime FechaEscritura { get; set; }

        //[Required(ErrorMessage = "El campo \"Observaciones\" es requerido favor de especificar.")]
        //[StringLength(300, ErrorMessage = "El largo máximo es de 300 caracteres.")]
        //[Display(Name = "Descripción de la Escritura:")]
        //public string Observaciones { get; set; }

        //public int Id_Tipo_Documento { get; set; }

        //public int Id_Estatus { get; set; }

        //public string Email { get; set; }

        //public string Accion { get; set; }

        //public string prevAccion { get; set; }

        //public string prevControlador { get; set; }
    }
}
