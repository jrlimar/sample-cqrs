using Dev.Core.Messages.IntegrationEvents;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Retry.Simple;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dev.Services.Handlers
{
    public class PedidoRealizadoEventHandler : IHandleMessages<PedidoRealizadoEvent>/*, IHandleMessages<IFailed<PedidoRealizadoEvent>>*/
    {
        private readonly IBus bus;

        public PedidoRealizadoEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async Task Handle(PedidoRealizadoEvent message)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"Pedido Recebido API - {message.Texto}");
            Console.ForegroundColor = ConsoleColor.Black;

            Thread.Sleep(5000);

            if (message.NomeErro == "erro")
                throw new Exception("Forçado erro na API");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Pedido Enviado API - {message.Texto}");
            Console.ForegroundColor = ConsoleColor.Black;

            await Task.CompletedTask;
        }

        //public async Task Handle(IFailed<PedidoRealizadoEvent> failed)
        //{
        //    try
        //    {
        //        Console.ForegroundColor = ConsoleColor.DarkBlue;
        //        Console.WriteLine($"Erro retry depois de 10 segundos");
        //        Console.ForegroundColor = ConsoleColor.Black;

        //        await bus.DeferLocal(TimeSpan.FromSeconds(10), failed.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine($"ERRO RETRY - {ex.Message}");
        //        Console.ForegroundColor = ConsoleColor.Black;
        //    }
        //}
    }
}
