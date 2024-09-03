using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Domain.MedicalRecord.Entity;

namespace Trasen.PaperFree.Domain.MedicalRecord.Repository
{
    public interface IInpatientInfoRepo
    {
        IQueryable<InpatientInfo> QueryAll();

        ValueTask<InpatientInfo?> FindById(string Id);

        bool Update(InpatientInfo entity);

        bool Update(List<InpatientInfo> entity);
    }
}