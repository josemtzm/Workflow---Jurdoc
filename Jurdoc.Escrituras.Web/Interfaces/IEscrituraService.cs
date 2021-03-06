﻿using Jurdoc.Escrituras.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jurdoc.Escrituras.Web.Interfaces
{
    public interface IEscrituraService
    {
        IEnumerable<Escritura> GetEscrituras();
        Escritura GetEscritura(int ID_ESCRITURA);
        void AddEscritura(Escritura escritura);
        void EditEscritura(Escritura escritura);
        void DeleteEscritura(Escritura escritura);
        //void AprobarEscritura(int ID_ESCRITURA);
        //void RechazarEscritura(int ID_ESCRITURA, string Observaciones);
    }
}
