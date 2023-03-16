using MediatR;
using System;

namespace Domain.Core.Notifications
{
    public class DomainNotification : INotification
    {
        public Guid DomainNotificationId { get; private set; }
        public Guid AggregateId { get; protected set; }
        public DateTime Timestamp { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public string MessageType { get; protected set; }
        public int Version { get; private set; }

        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
            Version = 1;

            Timestamp = DateTime.Now;
            MessageType = GetType().Name;
        }

        public DomainNotification()
        {
            Timestamp = DateTime.Now;
            MessageType = GetType().Name;
        }
    }
}
