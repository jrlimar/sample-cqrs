using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Dev.Domain.DomainEvents
{
    public class ProdutoDomainEventHandler : INotificationHandler<ProdutoAcimaEsperadoDomainEvent>
    {
        public async Task Handle(ProdutoAcimaEsperadoDomainEvent notification, CancellationToken cancellationToken)
        {
            // Enviar um email...

            await Task.CompletedTask;
        }
    }
}
