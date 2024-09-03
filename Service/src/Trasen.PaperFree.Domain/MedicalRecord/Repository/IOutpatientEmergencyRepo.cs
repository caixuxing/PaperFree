using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Domain.MedicalRecord.Entity;

namespace Trasen.PaperFree.Domain.MedicalRecord.Repository
{
    public interface IOutpatientEmergencyRepo
    {
        Task<bool> AddAsync(OutpatientEmergency entity, CancellationToken cancellationToken);
        IQueryable<OutpatientEmergency> QueryAll();

        ValueTask<OutpatientEmergency?> FindById(string Id);

        bool Update(OutpatientEmergency entity);
    }
}
