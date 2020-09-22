using Jurdoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jurdoc.Interface
{
    public interface IDocumentoEscrituraService
    {
        IEnumerable<DocumentoEscritura> GetDocEscrituras();
        DocumentoEscritura GetDocEscritura(int ID_ESCRITURA);
        void AddDocEscritura(DocumentoEscritura escritura);
        void EditDocEscritura(DocumentoEscritura escritura);
        void DeleteDocEscritura(DocumentoEscritura escritura);
    }
}
