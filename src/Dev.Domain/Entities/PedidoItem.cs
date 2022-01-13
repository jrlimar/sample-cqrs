using Dev.Core.Entities;

namespace Dev.Domain.Entities
{
    public class PedidoItem : Entity
    {
        public PedidoItem(int quantidade, decimal valorUnitario)
        {
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
    }
}
