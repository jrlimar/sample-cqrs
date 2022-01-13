using Dev.Api.Commands;
using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;
using System;
using System.Threading.Tasks;

namespace Dev.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoRebusController : Controller
    {
        private readonly IBus bus;

        public PedidoRebusController( IBus bus)
        {
            this.bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> EnviarPedido(string nome, int qtd, decimal valor)
        {
            var command = new AdicionarPedidoRebusCommand(nome, qtd, valor);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Pedido Publicado! {command.Timestamp}");
            Console.ForegroundColor = ConsoleColor.Black;

            await bus.Send(command);

            return Ok();
        }
    }
}
