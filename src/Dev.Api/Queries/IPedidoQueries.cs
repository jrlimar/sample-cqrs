using Dev.Api.Queries.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dev.Api.Queries
{
    public interface IPedidoQueries
    {
        Task<Carrinho> ObterCarrinhoCliente(Guid clienteId);
        Task<IEnumerable<Pedido>> ObterPedidosCliente(Guid clienteId);
    }
}
