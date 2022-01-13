using Dev.Domain.Entities;
using Dev.Domain.Enums;
using Dev.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Dev.Repositories.SqlServer
{
    public class PedidoRepository : IPedidoRepository
    {
        public void Adicionar(Pedido pedido)
        {
            
        }

        public async Task<Pedido> ObterPorId(Guid id)
        {
            var pedido = new Pedido(777, Status.Rascunho, null);

            return await Task.FromResult(pedido);
        }
    }
}
