using Dev.Core.Messages;

namespace Dev.Api.Commands
{
    public class AdicionarPedidoCommand : Command
    {
        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public AdicionarPedidoCommand(string nome, int quantidade, decimal valorUnitario)
        {
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public override bool IsValid()
        {
            ValidationResult = new AdicionarPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
