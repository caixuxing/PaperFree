using MediatR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trasen.PaperFree.Domain.SeedWork;
using Trasen.PaperFree.Domain.Shared.Entity.Interfances;

namespace Trasen.PaperFree.Domain.Base
{
    public record FullRoot : IDomainEvents, IHasCreated<string>, IHasModified<string?>, IHasVersion
    {
        [Required, Key]
        public string Id { get; set; } = Guid.Empty.ToString();
        public string CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string? LastModifyId { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public bool IsDeleted { get; set; }

        public FullRoot ChangeDelete()
        {
            this.IsDeleted = true;
            return this;
        }

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