using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.MedicalRecord.Commands.PatientDetails;
using Trasen.PaperFree.Domain.Base;
using Trasen.PaperFree.Domain.PatientDetails.Repository;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.PatientDetails
{
    internal class ModifyFileNameHandlers : IRequestHandler<ModifyFileNameCmd, bool>
    {
        private readonly IFilesOtherRepo _filesOtherRepo;
        private readonly IFilesHisRepo _filesHisRepo;
        private readonly Validate<ModifyFileNameCmd> validate;
        private readonly IUnitOfWork _unitOfWork;

        public ModifyFileNameHandlers(IFilesOtherRepo filesOtherRepo, IFilesHisRepo filesHisRepo, Validate<ModifyFileNameCmd> validate, IUnitOfWork unitOfWork)
        {
            _filesOtherRepo = filesOtherRepo;
            _filesHisRepo = filesHisRepo;
            this.validate = validate;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ModifyFileNameCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entityother = await _filesOtherRepo.QueryAll().FirstOrDefaultAsync(x=>x.FileId==request.FileId);
            if (entityother is not null)
            {
                entityother.ChangeFilesOther(request.FILE_SAVENAME);
                _filesOtherRepo.Update(entityother);
            }
            var entityhis = await _filesHisRepo.QueryAll().FirstOrDefaultAsync(x => x.FileId == request.FileId);
            if (entityhis is not null)
            {
                entityhis.changFilesHis(request.FILE_SAVENAME);
                _filesHisRepo.Update(entityhis);
            }
            if (entityhis is null&& entityother is null) throw new BusinessException(MessageType.Error, "更新失败!", "当前文件数据不存在!");
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
