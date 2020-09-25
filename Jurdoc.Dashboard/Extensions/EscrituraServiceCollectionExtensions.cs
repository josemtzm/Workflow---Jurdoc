using Jurdoc.Dashboard.Activities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jurdoc.Dashboard.Extensions
{
    public static class EscrituraServiceCollectionExtensions
    {
        public static IServiceCollection AddEscriturasActivities(this IServiceCollection services)
        {
            return services
                .AddActivity<AgregarEscritura>()
                //.AddActivity<AprobarEscritura>()
                //.AddActivity<RechazarEscritura>()
                ;
        }
    }
}
