using FluentValidation;

namespace Dev.Api.Commands
{
    public class AdicionarPedidoValidation : AbstractValidator<AdicionarPedidoCommand>
    {
        public AdicionarPedidoValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade miníma de um item é 1");

            RuleFor(c => c.ValorUnitario)
                .GreaterThan(0)
                .WithMessage("O valor do item precisa ser maior que 0");
        }
    }
}
