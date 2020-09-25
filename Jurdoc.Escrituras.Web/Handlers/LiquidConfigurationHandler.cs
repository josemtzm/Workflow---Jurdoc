using System.Threading;
using System.Threading.Tasks;
using Elsa.Scripting.Liquid.Messages;
using Fluid;
using Jurdoc.Escrituras.Web.Models;
using MediatR;


namespace Jurdoc.Escrituras.Web.Handlers
{
    // <summary>
    /// Configure the Liquid template context to allow access to certain models. 
    /// </summary>
    public class LiquidConfigurationHandler : INotificationHandler<EvaluatingLiquidExpression>
    {
        public Task Handle(EvaluatingLiquidExpression notification, CancellationToken cancellationToken)
        {
            var context = notification.TemplateContext;
            context.MemberAccessStrategy.Register<Escritura>();

            return Task.CompletedTask;
        }
    }
}
