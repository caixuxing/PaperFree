using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.MedicalRecord.Commands.OutpatientInfo;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;
using Trasen.PaperFree.Domain.MedicalRecord.Entity;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.OutpatientInfo
{
    internal sealed class CreateOutpatientInfoHandler : IRequestHandler<CreateOutpatientInfoTableCmd, string>
    {
        private readonly IOutpatientInfoRepo outpatientInfoRepo;
        private readonly Validate<CreateOutpatientInfoTableCmd> validate;
        private readonly IGuidGenerator guidGenerator;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUser currentUser;

        public CreateOutpatientInfoHandler(IOutpatientInfoRepo outpatientInfoRepo, Validate<CreateOutpatientInfoTableCmd> validate, IGuidGenerator guidGenerator, IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            this.outpatientInfoRepo = outpatientInfoRepo;
            this.validate = validate;
            this.guidGenerator = guidGenerator;
            this.unitOfWork = unitOfWork;
            this.currentUser = currentUser;
        }

        public async Task<string> Handle(CreateOutpatientInfoTableCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entity = new Trasen.PaperFree.Domain.MedicalRecord.Entity.OutpatientInfo(
                guidGenerator.Create().ToString(), guidGenerator.Create().ToString(), request.PatientId, request.AdmissId, guidGenerator.Create().ToString(), request.VisitId, request.OrgCode, request.HospCode
                , request.InputCode, request.Name, request.SexType, request.DateOfBirth, request.Age, request.IdCard, request.EnterDate, request.OutDate, request.EnterDeptCode
                , request.OutDeptCode, request.OutWardCode, request.DoctorKzrCode, request.DoctorZrysCode, request.DoctorZyysCode, request.DoctorZzysCode, request.ChargeNurseCode,
                request.BasyStatus, DateTime.Now, request.CaseType, "0");
            await outpatientInfoRepo.AddAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "创建病案成功";
        }
    }
}
