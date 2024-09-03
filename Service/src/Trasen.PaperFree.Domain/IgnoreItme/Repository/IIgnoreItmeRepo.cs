using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Domain.IgnoreItme.Entity;
using Trasen.PaperFree.Domain.MedicalRecord.Entity;

namespace Trasen.PaperFree.Domain.IgnoreItme.Repository
{
    public interface IIgnoreItmeRepo
    {
        Task<bool> AddAsync(IgnoreItmeTable entity, CancellationToken cancellationToken);
        IQueryable<IgnoreItmeTable> QueryAll();

        ValueTask<IgnoreItmeTable?> FindById(string Id);

        bool Update(IgnoreItmeTable entity);
    }
}
