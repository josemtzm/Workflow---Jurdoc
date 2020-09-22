using Jurdoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jurdoc.Interface
{
    public interface IEscrituraService
    {
        IEnumerable<Escritura> GetEscrituras();
        Escritura GetEscritura(int ID_ESCRITURA);
        void AddEscritura(Escritura escritura);
        void EditEscritura(Escritura escritura);
        void DeleteEscritura(Escritura escritura);
    }
}
