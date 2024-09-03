using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ProcessRecord
{
    internal sealed class ModifyProcessDesignHandler : IRequestHandler<ModifyProcessDesignCmd, bool>
    {
        private readonly IProcessDesignRepo processDesignRepo;
        private readonly Validate<ModifyProcessDesignCmd> validate;
        private readonly IUnitOfWork unitOfWork;

        public ModifyProcessDesignHandler(
            IProcessDesignRepo processDesignRepo,
            Validate<ModifyProcessDesignCmd> validate,
            IUnitOfWork unitOfWork)
        {
            this.processDesignRepo = processDesignRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ModifyProcessDesignCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var entity = await processDesignRepo.FindById(request.Id);
            if (entity is null) throw new BusinessException(MessageType.Error, "更新失败!", "流程设计实体不存在!");
            entity.ChangeProcessDesign(
                request.ProcessName,
                request.IsEnable,
                request.DeptCode,
                request.processTempType,
                request.OrgCode,
                request.HospCode);

            processDesignRepo.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}