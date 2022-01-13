using Dev.Core.Messages;
using System;

namespace Dev.Api.Events
{
    public class PedidoAprovadoEvent : Event
    {
        public PedidoAprovadoEvent(Guid clienteId, Guid pedidoId)
        {
            ClienteId = clienteId;
            PedidoId = pedidoId;
        }

        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }
    }
}
