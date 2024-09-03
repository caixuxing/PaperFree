using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.MedicalRecord.Commands.OutpatientInfo;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.OutpatientInfo
{
    internal class ModifyOutpatientEmergencyHandler : IRequestHandler<ModifyOutpatientEmergencyCmd, bool>
    {
        private readonly IOutpatientEmergencyRepo _repo;
        private readonly Validate<ModifyOutpatientEmergencyCmd> validate;
        private readonly IUnitOfWork _unitOfWork;

        public ModifyOutpatientEmergencyHandler(IOutpatientEmergencyRepo repo, Validate<ModifyOutpatientEmergencyCmd> validate, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            this.validate = validate;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ModifyOutpatientEmergencyCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entity = await _repo.QueryAll().FirstOrDefaultAsync(x=>x.ArchiveId==request.ArchiveId);
            if (entity is null ) throw new BusinessException(MessageType.Error, "更新失败!", "当前患者数据不存在!");
            entity.UpdateoutpatientEmergency(request.HospRecordId, request.OrgCode, request.HospCode, request.Name, request.SexType, request.DateOfBirth,
                request.Age, request.IdCard, request.SeePatientsDate, request.SeeDeptCode, request.ReceiveDoctorCode, request.IcdCode, request.IcdName, request.AdmissId);
            _repo.Update(entity);
           await _unitOfWork.SaveChangesAsync(cancellationToken);
          return true;
        }
    }
}
