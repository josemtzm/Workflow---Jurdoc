using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elsa;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Extensions;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;
using Jurdoc.Escrituras.Web.Interfaces;
using Jurdoc.Escrituras.Web.Models;

namespace Jurdoc.Escrituras.Web.Activities
{
    [ActivityDefinition(Category = "Escrituras", Description = "Agregar Escritura", Icon = "fas fa-file-alt", Outcomes = new[] { OutcomeNames.Done })]
    public class AgregarEscritura : Activity
    {
        IEscrituraService EscrituraService;
        public AgregarEscritura(IEscrituraService _EscrituraService)
        {
            EscrituraService = _EscrituraService;
        }

        [ActivityProperty(Hint = "IdEscritura.")]
        public WorkflowExpression<int> IdEscritura
        {
            get => GetState<WorkflowExpression<int>>();
            set => SetState(value);
        }
        [ActivityProperty(Hint = "NumeroEscritura.")]
        public WorkflowExpression<string> NumeroEscritura
        {
            get => GetState<WorkflowExpression<string>>();
            set => SetState(value);
        }
        [ActivityProperty(Hint = "Solicitante.")]
        public WorkflowExpression<string> Solicitante
        {
            get => GetState<WorkflowExpression<string>>();
            set => SetState(value);
        }
        [ActivityProperty(Hint = "FechaEscritura.")]
        public WorkflowExpression<DateTime> FechaEscritura
        {
            get => GetState<WorkflowExpression<DateTime>>();
            set => SetState(value);
        }
        [ActivityProperty(Hint = "Observaciones.")]
        public WorkflowExpression<string> Observaciones
        {
            get => GetState<WorkflowExpression<string>>();
            set => SetState(value);
        }
        [ActivityProperty(Hint = "Email.")]
        public WorkflowExpression<string> Email
        {
            get => GetState<WorkflowExpression<string>>();
            set => SetState(value);
        }

        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken)
        {
            
            var esc = new Escritura
            {
                IdEscritura = await context.EvaluateAsync(IdEscritura, cancellationToken),
                NumeroEscritura = await context.EvaluateAsync(NumeroEscritura, cancellationToken),
                Solicitante = await context.EvaluateAsync(Solicitante, cancellationToken),
                FechaEscritura = await context.EvaluateAsync(FechaEscritura, cancellationToken),
                Observaciones = await context.EvaluateAsync(Observaciones, cancellationToken),
                //Id_Tipo_Documento = await context.EvaluateAsync(Id_Tipo_Documento, cancellationToken),
                //Id_Estatus = await context.EvaluateAsync(Id_Estatus, cancellationToken),
                Email = await context.EvaluateAsync(Email, cancellationToken)
            };

            EscrituraService.AddEscritura(esc);

            Output.SetVariable("Escritura", esc);
            return Done();
        }

    }
}
