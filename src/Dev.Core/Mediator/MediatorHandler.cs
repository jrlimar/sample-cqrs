using Dev.Core.Messages;
using Dev.Core.Messages.DomainEvents;
using Dev.Core.Messages.LogEvents;
using Dev.Core.Messages.Notifications;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dev.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator mediator;

        public MediatorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await mediator.Publish(@event);
        }

        public async Task PublishDomainEvent<T>(T notification) where T : DomainEvent
        {
            await mediator.Publish(notification);
        }

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            return await mediator.Send(command);
        }

        public async Task PublishLogEvent<T>(T log) where T : LogEvent
        {
            await mediator.Publish(log);
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await mediator.Publish(notification);
        }

        public async Task PublishEvents<T>(IList<T> @events) where T : Event
        {
            foreach (var @event in @events)
            {
                await mediator.Publish(@event);
            }
        }
    }
}
