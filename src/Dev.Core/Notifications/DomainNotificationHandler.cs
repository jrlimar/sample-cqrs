using Dev.Core.Messages.Notifications;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.Core.Notifications
{
    public class DomainNotificationHandler : IDomainNotification
    {
        private List<DomainNotification> notificacoes;

        public DomainNotificationHandler()
        {
            notificacoes = new List<DomainNotification>();
        }

        public async Task Handle(DomainNotification notificacao)
        {
            notificacoes.Add(notificacao);
            await Task.CompletedTask;
        }

        public async Task Notificar(string key, string value)
        {
            await Handle(new DomainNotification(key, value));
        }

        public void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar("error", error.ErrorMessage);
            }
        }

        public List<DomainNotification> ObterNotificacoes()
        {
            return notificacoes;
        }

        public bool TemNotificacao()
        {
            return notificacoes.Any();
        }
    }
}
