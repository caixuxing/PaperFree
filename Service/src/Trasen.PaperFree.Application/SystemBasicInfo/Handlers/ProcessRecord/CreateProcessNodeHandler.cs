using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;
using Trasen.PaperFree.Domain.ProcessRecord.Entity;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ProcessRecord
{
    internal sealed class CreateProcessNodeHandler : IRequestHandler<CreateProcessNodeCmd, bool>
    {
        private readonly IProcessDesignRepo processDesignRepo;
        private readonly IProcessNodeRepo processNodeRepo;
        private readonly Validate<CreateProcessNodeCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly IGuidGenerator _guidGenerator;

        public CreateProcessNodeHandler(
            IProcessNodeRepo processNodeRepo,
            Validate<CreateProcessNodeCmd> validate,
            IUnitOfWork unitOfWork,
            IGuidGenerator guidGenerator,
            IProcessDesignRepo processDesignRepo)
        {
            this.processNodeRepo = processNodeRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            _guidGenerator = guidGenerator;
            this.processDesignRepo = processDesignRepo;
        }

        public async Task<bool> Handle(CreateProcessNodeCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var flag = await processDesignRepo.QueryAll().AsTracking().AnyAsync(x => x.Id == request.ProcessDesignId);
            if (!flag) throw new BusinessException(MessageType.Warn, "非法流程设计!", "流程设计主键ID不存在!");
            string id = _guidGenerator.Create().ToString();
            var entity = new ProcessNode(id,
                request.ProcessDesignId,
                request.NodeName,
                request.NodeCode,
                request.UpperNodeId,
                request.LowerNodeId,
                string.Join(",", request.EventDirectionBranch),
                request.IsRejectToNode,
                request.OderNo,
                request.CurrentNodeApprovers.Select(x => new Domain.ProcessRecord.Entity.NodeApprover(
                    _guidGenerator.Create().ToString(),
                    id,
                    x.ApproverId,
                    x.ApproverAccount,
                    x.ApproverName
                    )).ToList(),
                request.NodeMapWorkflowStatus);
            await processNodeRepo.AddAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}