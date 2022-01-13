using Dev.Api.Commands;
using Dev.Core.Messages.IntegrationEvents;
using Rebus.Bus;
using Rebus.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dev.Api.RebusHandlers
{
    public class PedidoRebusCommandHandler : IHandleMessages<AdicionarPedidoRebusCommand>
    {
        private readonly IBus bus;
        public PedidoRebusCommandHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(AdicionarPedidoRebusCommand command)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Pedido Em Andamento - Mensagem {command.AggregateId} - {DateTime.Now}");
            Console.ForegroundColor = ConsoleColor.Black;

            Thread.Sleep(10000);

            if (command.Quantidade == 5)
                throw new Exception("Erro forçado");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Pedido Processado! - Mensagem {command.AggregateId} - {DateTime.Now}");
            Console.ForegroundColor = ConsoleColor.Black;

            await bus.Publish(new PedidoRealizadoEvent($"{command.AggregateId} - {DateTime.Now}", ""/*forçar erro*/));

            await Task.CompletedTask;
        }
    }
}
