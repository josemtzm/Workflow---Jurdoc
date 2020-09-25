using Elsa;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;
using Jurdoc.Escrituras.Web.Interfaces;
using Jurdoc.Escrituras.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jurdoc.Escrituras.Web.Activities
{
    [ActivityDefinition(Category = "Escrituras", Description = "Rechazar Escritura", Icon = "fas fa-times", Outcomes = new[] { OutcomeNames.Done })]
    public class RechazarEscritura : Activity
    {
        IEscrituraService EscrituraService;
        public RechazarEscritura(IEscrituraService _EscrituraService)
        {
            EscrituraService = _EscrituraService;
        }

        [ActivityProperty(Hint = "IdEscritura.")]
        public WorkflowExpression<int> IdEscritura
        {
            get => GetState<WorkflowExpression<int>>();
            set => SetState(value);
        }

        [ActivityProperty(Hint = "Observaciones.")]
        public WorkflowExpression<string> Observaciones
        {
            get => GetState<WorkflowExpression<string>>();
            set => SetState(value);
        }

        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken)
        {
            Escritura esc = EscrituraService.GetEscritura(await context.EvaluateAsync(IdEscritura, cancellationToken));

            esc.Id_Estatus = 50;
            esc.Observaciones = await context.EvaluateAsync(Observaciones, cancellationToken);

            EscrituraService.EditEscritura(esc);

            Output.SetVariable("Escritura", esc);
            return Done();
        }
    }
}
