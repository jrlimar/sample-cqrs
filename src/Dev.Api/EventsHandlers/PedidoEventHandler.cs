using Dev.Api.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Dev.Api.EventsHandlers
{
    public class PedidoEventHandler : INotificationHandler<PedidoIniciadoEvent>, INotificationHandler<PedidoAprovadoEvent>
    {
        public async Task Handle(PedidoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;        
        }

        public async Task Handle(PedidoAprovadoEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
