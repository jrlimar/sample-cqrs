using Dev.Core.Data;
using Dev.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Dev.Domain.Interfaces
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<Pedido> ObterPorId(Guid id);
        void Adicionar(Pedido pedido);

    }
}
