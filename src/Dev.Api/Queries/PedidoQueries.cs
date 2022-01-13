using Dev.Api.Queries.DTO;
using Dev.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dev.Api.Queries
{
    public class PedidoQueries : IPedidoQueries
    {

        private readonly IPedidoRepository pedidoRepository;

        public PedidoQueries(IPedidoRepository pedidoRepository)
        {
            this.pedidoRepository = pedidoRepository;
        }

        public async Task<Carrinho> ObterCarrinhoCliente(Guid clienteId)
        {
            var pedido = await pedidoRepository.ObterPorId(clienteId);
            var carrinho = new Carrinho { ClienteId = clienteId, PedidoId = pedido.Id, SubTotal = 0, ValorDesconto = 0, ValorTotal = 1000, VoucherCodigo = "ajkjskf893934589734574" };

            return carrinho;
        }

        public async Task<IEnumerable<Pedido>> ObterPedidosCliente(Guid clienteId)
        {
            var pedidos = new List<Pedido>();

            pedidos.Add(new Pedido
            {
                Id = Guid.NewGuid(),
                ValorTotal = 5000,
                PedidoStatus = 1,
                Codigo = 666666666,
                DataCadastro = DateTime.Now
            });

            pedidos.Add(new Pedido
            {
                Id = Guid.NewGuid(),
                ValorTotal = 678,
                PedidoStatus = 1,
                Codigo = 87898989,
                DataCadastro = DateTime.Now
            });

            return await Task.FromResult(pedidos);
        }
    }
}
