using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using Trasen.PaperFree.Domain.SeedWork;
using Trasen.PaperFree.Domain.Shared.Entity.Interfances;

namespace Trasen.PaperFree.Domain.Base
{
    public record Entity : IDomainEvents, IHasCreated<string>
    {
        public string CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public string Id { get; set; } = Guid.Empty.ToString();

        public bool IsDeleted { get; set; }

        [NotMapped]
        private List<INotification> domainEvents = new();

        public void AddDomainEvent(INotification eventItem)
        {
            domainEvents.Add(eventItem);
        }

        public void AddDomainEventIfAbsent(INotification eventItem)
        {
            if (!domainEvents.Contains(eventItem))
            {
                domainEvents.Add(eventItem);
            }
        }
        public void ClearDomainEvents()
        {
            domainEvents.Clear();
        }

        public IEnumerable<INotification> GetDomainEvents()
        {
            return domainEvents;
        }
    }
}