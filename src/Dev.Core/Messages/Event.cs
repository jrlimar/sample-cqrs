using MediatR;
using System;

namespace Dev.Core.Messages
{
    public abstract class Event : Message, INotification
    {
        public DateTime TimeStamp { get; protected set; }

        protected Event()
        {
            this.TimeStamp = DateTime.Now;
        }
    }
}
