namespace Dev.Core.Messages.IntegrationEvents
{
    public class PedidoRealizadoEvent : IntegrationEvent
    {
        public string NomeErro { get; set; }
        public string Texto { get; private set; }
        public PedidoRealizadoEvent(string texto, string nomeErro)
        {
            Texto = texto;
            NomeErro = nomeErro;
        }
    }
}
