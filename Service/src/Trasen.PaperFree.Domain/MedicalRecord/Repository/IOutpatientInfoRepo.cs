using Trasen.PaperFree.Domain.MedicalRecord.Entity;

namespace Trasen.PaperFree.Domain.MedicalRecord.Repository
{
    public interface IOutpatientInfoRepo
    {
        Task<bool> AddAsync(OutpatientInfo entity, CancellationToken cancellationToken);
        IQueryable<OutpatientInfo> QueryAll();

        ValueTask<OutpatientInfo?> FindById(string Id);

        bool Update(OutpatientInfo entity);

        bool Update(List<OutpatientInfo> entity);
    }
}