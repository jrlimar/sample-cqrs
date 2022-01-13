using Dev.Core.Messages.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dev.Core.Notifications
{
    public interface IDomainNotification 
    {
        bool TemNotificacao();
        List<DomainNotification> ObterNotificacoes();
        Task Handle(DomainNotification notificacao);
        Task Notificar(string key, string value);
    }
}
