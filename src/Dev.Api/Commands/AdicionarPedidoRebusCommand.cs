using Dev.Core.Messages;

namespace Dev.Api.Commands
{
    public class AdicionarPedidoRebusCommand : Command
    {
        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public AdicionarPedidoRebusCommand(string nome, int quantidade, decimal valorUnitario)
        {
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
