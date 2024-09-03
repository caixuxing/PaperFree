using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;
using Trasen.PaperFree.Domain.ProcessRecord.Entity;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ProcessRecord
{
    internal sealed class ModifyProcessNodeHandler : IRequestHandler<ModifyProcessNodeCmd, bool>
    {
        private readonly IProcessDesignRepo processDesignRepo;
        private readonly IProcessNodeRepo processNodeRepo;
        private readonly Validate<ModifyProcessNodeCmd> validate;
        private readonly IUnitOfWork unitOfWork;

        private readonly IGuidGenerator _guidGenerator;

        public ModifyProcessNodeHandler(IProcessNodeRepo processNodeRepo, Validate<ModifyProcessNodeCmd> validate, IUnitOfWork unitOfWork, IGuidGenerator guidGenerator, IProcessDesignRepo processDesignRepo)
        {
            this.processNodeRepo = processNodeRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            _guidGenerator = guidGenerator;
            this.processDesignRepo = processDesignRepo;
        }

        public async Task<bool> Handle(ModifyProcessNodeCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var flag = await processDesignRepo.QueryAll().AsTracking().AnyAsync(x => x.Id == request.ProcessDesignId);
            if (!flag) throw new BusinessException(MessageType.Warn, "非法流程设计!", "流程设计主键ID不存在!");
            var entity = await processNodeRepo.FindById(request.Id);
            if (entity is null) throw new BusinessException(MessageType.Error, "更新失败！", "流程节点信息不存在");
            entity.SetIsRejectToNode(request.IsRejectToNode)
                .SetEventDirectionBranch(string.Join(",", request.EventDirectionBranch))
                .SetNodeName(request.NodeName)
                .SetNodeCode(request.NodeCode)
                .SetNodeMapWorkflowStatus(request.NodeMapWorkflowStatus)
                .SetLowerNodeId(request.LowerNodeId)
                .SetUpperNodeId(request.UpperNodeId)
                .SetProcessDesignId(request.ProcessDesignId)
                .SetOderNo(request.OderNo)
                .SetNodeApprovers(request.CurrentNodeApprovers.Select(x => new NodeApprover(_guidGenerator.Create().ToString(), entity.Id, x.ApproverId, x.ApproverAccount, x.ApproverName)).ToList());
            await processNodeRepo.Update(entity);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}