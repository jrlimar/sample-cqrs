using Dev.Core.Entities;
using Dev.Domain.Enums;
using System.Collections.Generic;

namespace Dev.Domain.Entities
{
    public class Pedido : Entity, IAggregationRoot
    {
        public int Codigo { get; private set; }
        public Status PedidoStatus { get; private set; }

        private readonly List<PedidoItem> pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => pedidoItems;

        public Pedido(int codigo, Status pedidoStatus, List<PedidoItem> pedidoItems)
        {
            this.Codigo = codigo;
            this.PedidoStatus = pedidoStatus;
            this.pedidoItems = pedidoItems;
        }

    }
}
