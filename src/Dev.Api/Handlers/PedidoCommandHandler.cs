using Dev.Api.Commands;
using Dev.Api.Events;
using Dev.Core.Mediator;
using Dev.Core.Messages;
using Dev.Core.Messages.Notifications;
using Dev.Core.Notifications;
using Dev.Domain.DomainEvents;
using Dev.Domain.Entities;
using Dev.Domain.Enums;
using Dev.Domain.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dev.Api.Handlers
{
    public class PedidoCommandHandler : IRequestHandler<AdicionarPedidoCommand, bool>
    {
        private readonly IMediatorHandler mediatorHandler;
        private readonly IDomainNotification domainNotification;
        private readonly IPedidoRepository pedidoRepository;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository, IMediatorHandler mediatorHandler, IDomainNotification domainNotification)
        {
            this.pedidoRepository = pedidoRepository;
            this.mediatorHandler = mediatorHandler;
            this.domainNotification = domainNotification;
        }

        public async Task<bool> Handle(AdicionarPedidoCommand command, CancellationToken cancellationToken)
        {
            if (await ValidarComando(command))
                return false;

            var pedido = new Pedido(111, Status.Iniciado, null);

            pedidoRepository.Adicionar(pedido);

            //event 1
            pedido.AdicionarEvento(new PedidoIniciadoEvent(Guid.NewGuid(), pedido.Id));

            //domain event
            if (command.Quantidade > 100)
            {
                await mediatorHandler.PublishDomainEvent(new ProdutoAcimaEsperadoDomainEvent(pedido.Id, command.Quantidade));
            }

            //event 2
            pedido.AdicionarEvento(new PedidoAprovadoEvent(Guid.NewGuid(), pedido.Id));

            //deixa depois do commit
            await mediatorHandler.PublishEvents(pedido.Events.ToList());

            return await Task.FromResult(true);
        }

        private async Task<bool> ValidarComando(Command command)
        {
            if (command.IsValid()) 
                return false;

            //notificações
            foreach (var error in command.ValidationResult.Errors)
            {
                await domainNotification.Handle(new DomainNotification(command.MessageType, error.ErrorMessage));
            }

            return true;
        }
    }
}
