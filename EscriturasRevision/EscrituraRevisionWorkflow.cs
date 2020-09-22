using Elsa;
using Elsa.Activities;
using Elsa.Activities.ControlFlow.Activities;
using Elsa.Activities.Email.Activities;
using Elsa.Activities.Http.Activities;
using Elsa.Activities.Timers.Activities;
using Elsa.Activities.Workflows.Activities;
using Elsa.Expressions;
using Elsa.Scripting.JavaScript;
using Elsa.Services;
using Elsa.Services.Models;
using System;
using System.Dynamic;
using System.Net;
using System.Net.Http;

namespace EscriturasRevision
{
    public class EscrituraRevisionWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .StartWith<ReceiveHttpRequest>(
                    x =>
                    {
                        x.Method = HttpMethod.Post.Method;
                        x.Path = new Uri("/escrituras", UriKind.Relative);
                        x.ReadContent = true;
                    }
                )
                .Then<SetVariable>(
                    x =>
                    {
                        x.VariableName = "Escritura";
                        x.ValueExpression = new JavaScriptExpression<ExpandoObject>("lastResult().Body");
                    }
                )
                .Then<SendEmail>(
                    x =>
                    {
                        x.From = new LiteralExpression("sygno.jmartinez@proveedores21b.com");
                        x.To = new JavaScriptExpression<string>("Escritura.solicitante.Email");
                        x.Subject =
                            new JavaScriptExpression<string>("`Escritura enviada por ${Escritura.solicitante.Nombre}`");
                        x.Body = new JavaScriptExpression<string>(
                            "`Escritura enviada por: ${Escritura.solicitante.Nombre} recibida para revision. " +
                            "<a href=\"${signalUrl('Approve')}\">Approbar</a> or <a href=\"${signalUrl('Reject')}\">Rechazar</a>`"
                        );
                    }
                )
                .Then<WriteHttpResponse>(
                    x =>
                    {
                        x.Content = new LiteralExpression(
                            "<h1>Revisión de Escritura</h1><p>La Escritura ha sido recibida y será revisada.</p>"
                        );
                        x.ContentType = "text/html";
                        x.StatusCode = HttpStatusCode.OK;
                        x.ResponseHeaders = new LiteralExpression("X-Powered-By=Elsa Workflows");
                    }
                )
                .Then<SetVariable>(
                    x =>
                    {
                        x.VariableName = "Approved";
                        x.ValueExpression = new LiteralExpression<bool>("false");
                    }
                )
                .Then<Fork>(
                    x => { x.Branches = new[] { "Approve", "Reject", "Remind" }; },
                    fork =>
                    {
                        fork
                            .When("Approve")
                            .Then<Signaled>(x => x.Signal = new LiteralExpression("Approve"))
                            .Then("Join");

                        fork
                            .When("Reject")
                            .Then<Signaled>(x => x.Signal = new LiteralExpression("Reject"))
                            .Then("Join");

                        fork
                            .When("Remind")
                            .Then<TimerEvent>(
                                x => x.TimeoutExpression = new LiteralExpression<TimeSpan>("00:00:10"),
                                name: "RemindTimer"
                            )
                            .Then<IfElse>(
                                x => x.ConditionExpression = new JavaScriptExpression<bool>("Escritura pendiente de revisión."),
                                ifElse =>
                                {
                                    ifElse
                                        .When(OutcomeNames.False)
                                        .Then<SendEmail>(
                                            x =>
                                            {
                                                x.From = new LiteralExpression("sygno.jmartinez@proveedores21b.com");
                                                x.To = new LiteralExpression("sygno.jmartinez@proveedores21b.com");
                                                x.Subject =
                                                    new JavaScriptExpression<string>(
                                                        "`${Escritura.solicitante.Nombre} tiene una escritura pendiente de revisión`"
                                                    );
                                                x.Body = new JavaScriptExpression<string>(
                                                    "`Número de Escritura: ${Escritura.numeroEscritura}.<br/>" +
                                                    "`Observaciones: ${Escritura.observaciones}.<br/>" +
                                                    "<a href=\"${signalUrl('Approve')}\">Approbar</a> or <a href=\"${signalUrl('Reject')}\">Rechazar</a>`"
                                                );
                                            }
                                        )
                                        .Then("RemindTimer");
                                }
                            );
                    }
                )
                .Then<Join>(x => x.Mode = Join.JoinMode.WaitAny, name: "Join")
                .Then<SetVariable>(
                    x =>
                    {
                        x.VariableName = "Approved";
                        x.ValueExpression = new JavaScriptExpression<object>("input('Signal') === 'Approve'");
                    }
                )
                .Then<IfElse>(
                    x => x.ConditionExpression = new JavaScriptExpression<bool>("Escritura Aprobada"),
                    ifElse =>
                    {
                        ifElse
                            .When(OutcomeNames.True)
                            .Then<SendEmail>(
                                x =>
                                {
                                    x.From = new LiteralExpression("sygno.jmartinez@proveedores21b.com");
                                    x.To = new JavaScriptExpression<string>("Escritura.solicitante.Email");
                                    x.Subject =
                                        new JavaScriptExpression<string>("`Escritura ${Escritura.Id} approved!`");
                                    x.Body = new JavaScriptExpression<string>(
                                        "`Estimado ${Escritura.solicitante.Nombre}, la Escritura ${Escritura.numeroEscritura} ha sido aprovada.`"
                                    );
                                }
                            );

                        ifElse
                            .When(OutcomeNames.False)
                            .Then<SendEmail>(
                                x =>
                                {
                                    x.From = new LiteralExpression("sygno.jmartinez@proveedores21b.com");
                                    x.To = new JavaScriptExpression<string>("Escritura.solicitante.Email");
                                    x.Subject =
                                        new JavaScriptExpression<string>("`Escritura ${Escritura.Id} rejected`");
                                    x.Body = new JavaScriptExpression<string>(
                                    "`Estimado ${Escritura.solicitante.Nombre}, la Escritura ${Escritura.numeroEscritura} ha sido aprovada.`" +
                                    "`Observaciones: ${Escritura.observaciones}.<br/>" 
                                    );
                                }
                            );
                    }
                );
        }
    }
}
