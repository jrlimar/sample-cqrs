using Dev.Core.Messages;
using Dev.Core.Messages.DomainEvents;
using Dev.Core.Messages.LogEvents;
using Dev.Core.Messages.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dev.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task PublishEvents<T>(IList<T> @events) where T : Event;
        Task PublishDomainEvent<T>(T notificacao) where T : DomainEvent;
        Task<bool> SendCommand<T>(T command) where T : Command;
        Task PublishLogEvent<T>(T log) where T : LogEvent;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}
