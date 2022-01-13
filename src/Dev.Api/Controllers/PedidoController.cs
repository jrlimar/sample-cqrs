using Dev.Api.Commands;
using Dev.Api.Queries;
using Dev.Core.Mediator;
using Dev.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Dev.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IMediatorHandler mediatorHandler;
        private readonly IPedidoQueries pedidoQueries;

        public PedidoController(IDomainNotification notification, IMediatorHandler mediatorHandler, IPedidoQueries pedidoQueries)
            : base(notification)
        {
            this.mediatorHandler = mediatorHandler;
            this.pedidoQueries = pedidoQueries;
        }

        [HttpPost]
        public async Task<IActionResult> EnviarPedido(string nome, int qtd, decimal valor)
        {
            var command = new AdicionarPedidoCommand(nome, qtd, valor);

            await mediatorHandler.SendCommand(command);

            return CustomResponse();
        }

        [HttpGet("Carrinho")]
        public async Task<IActionResult> BuscarCarrinho()
        {
            var carrinho =await pedidoQueries.ObterCarrinhoCliente(Guid.NewGuid());

            return CustomResponse(carrinho);
        }

        [HttpGet("Pedidos")]
        public async Task<IActionResult> BuscarPedidos()
        {
            var pedidos = await pedidoQueries.ObterPedidosCliente(Guid.NewGuid());

            return CustomResponse(pedidos);
        }
    }
}
