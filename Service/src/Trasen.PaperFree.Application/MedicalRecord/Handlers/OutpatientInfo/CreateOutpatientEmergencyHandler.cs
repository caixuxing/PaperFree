using DapperExtensions.Mapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Trasen.PaperFree.Application.MedicalRecord.Commands.OutpatientInfo;
using Trasen.PaperFree.Domain.MedicalRecord.Entity;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.OutpatientInfo
{
    internal class CreateOutpatientEmergencyHandler : IRequestHandler<CreateOutpatientEmergencyCmd, string>
    {
        private readonly IOutpatientEmergencyRepo outpatientEmergencyRepo;
        private readonly Validate<CreateOutpatientEmergencyCmd> validate;
        private readonly IGuidGenerator guidGenerator;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUser currentUser;

        public CreateOutpatientEmergencyHandler(IOutpatientEmergencyRepo  outpatientEmergency, Validate<CreateOutpatientEmergencyCmd> validate, IGuidGenerator guidGenerator, IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            this.outpatientEmergencyRepo = outpatientEmergency;
            this.validate = validate;
            this.guidGenerator = guidGenerator;
            this.unitOfWork = unitOfWork;
            this.currentUser = currentUser;
        }

        public async Task<string> Handle(CreateOutpatientEmergencyCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entity = new OutpatientEmergency(guidGenerator.Create().ToString(), request.HospRecordId, request.OrgCode, request.HospCode, request.Name, request.SexType, request.DateOfBirth,
                request.Age, request.IdCard, request.SeePatientsDate,request.SeeDeptCode, request.ReceiveDoctorCode, request.IcdCode, request.IcdName,request.AdmissId);
            await outpatientEmergencyRepo.AddAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "创建病历成功";
        }
    }
}
