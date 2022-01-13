using System;

namespace Dev.Core.Messages.LogEvents
{
    public class LogEvent : Event
    {
        public string Log { get; private set; }
        public DateTime Timestamp { get; private set; }

        public LogEvent(string log)
        {
            Log = log;
            Timestamp = DateTime.Now;
        }
    }
}
