using Trasen.PaperFree.Domain.SystemBasicData.Entity;

namespace Trasen.PaperFree.Domain.SystemBasicData.Repository
{
    public interface IDeptMenuTreeRepo
    {
        Task<bool> AddAsync(DeptMeMenuTreeEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AddAsyncList(List<DeptMeMenuTreeEntity> entity, CancellationToken cancellationToken);

        ValueTask<DeptMeMenuTreeEntity?> FindById(string Id);

        // Task<bool> Update(BaseWatermark entity);
        bool Update(DeptMeMenuTreeEntity entity);

        IQueryable<DeptMeMenuTreeEntity> QueryAll();

        /// <summary>
        /// 批处理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(List<DeptMeMenuTreeEntity> data);
    }
}