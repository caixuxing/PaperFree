using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Domain.SystemBasicData.Repository
{
    public interface IEssentialDocumentsRepo
    {
        Task<bool> AddAsync(EssentialDocuments entity, CancellationToken cancellationToken);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AddAsyncList(List<EssentialDocuments> entity, CancellationToken cancellationToken);

        ValueTask<EssentialDocuments?> FindById(string Id);

        // Task<bool> Update(BaseWatermark entity);
        bool Update(EssentialDocuments entity);

        IQueryable<EssentialDocuments> QueryAll();

        /// <summary>
        /// 批处理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(List<EssentialDocuments> data);
    }
}