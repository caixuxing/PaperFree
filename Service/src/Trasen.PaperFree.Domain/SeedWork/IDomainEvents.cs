using MediatR;

namespace Trasen.PaperFree.Domain.SeedWork
{
    /// <summary>
    /// 领域事件
    /// </summary>
    public interface IDomainEvents
    {
        /// <summary>
        /// 获取领域事件
        /// </summary>
        /// <returns></returns>
        IEnumerable<INotification> GetDomainEvents();

        /// <summary>
        /// 添加领域事件
        /// </summary>
        /// <param name="eventItem"></param>
        void AddDomainEvent(INotification eventItem);

        /// <summary>
        /// 如果已经存在这个元素，则跳过，否则增加。以避免对于同样的事件触发多次（比如在一个事务中修改领域模型的多个对象）
        /// </summary>
        /// <param name="eventItem"></param>
        void AddDomainEventIfAbsent(INotification eventItem);

        /// <summary>
        /// 清除
        /// </summary>
        public void ClearDomainEvents();
    }
}