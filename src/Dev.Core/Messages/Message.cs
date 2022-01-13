using Newtonsoft.Json;
using System;

namespace Dev.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            this.AggregateId = Guid.NewGuid();
            this.MessageType = GetType().Name;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
