using Dev.Core.Messages.DomainEvents;
using System;

namespace Dev.Domain.DomainEvents
{
    public class ProdutoAcimaEsperadoDomainEvent : DomainEvent
    {
        public int QuantidadeRecebida { get; private set; }
        public ProdutoAcimaEsperadoDomainEvent(Guid aggregateId, int quantidade) 
            : base(aggregateId)
        {
            QuantidadeRecebida = quantidade;
        }
    }
}
