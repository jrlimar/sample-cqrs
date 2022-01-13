using Dev.Core.Messages;
using System;
using System.Collections.Generic;

namespace Dev.Core.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        private List<Event> events;
        public IReadOnlyCollection<Event> Events => events?.AsReadOnly();

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }


        #region Events - acumula todos depois persiste ou faz alguma coisa

        public void AdicionarEvento(Event evento)
        {
            events = events ?? new List<Event>();
            events.Add(evento);
        }

        public void RemoverEvento(Event eventItem)
        {
            events?.Remove(eventItem);
        }

        public void LimparEventos()
        {
            events?.Clear();
        }

        #endregion

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}
