using EmployeeCleanArch.Domain.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeCleanArch.Domain.Common
{
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        //private readonly List<BaseEvent> _domainEvents = new();

        public TId Id { get; set; }

        /*[NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();*/
    }
}
