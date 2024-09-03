using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;
using Trasen.PaperFree.Domain.ProcessRecord.Entity;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using static ICSharpCode.SharpZipLib.Zip.ExtendedUnixData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ProcessRecord
{
    internal class ModifyWorkDesignHandler : IRequestHandler<ModifyWorkDesignCmd, bool>
    {

        readonly IProcessDesignRepo processDesignRepo;
        readonly IProcessNodeRepo  processNodeRepo;
        readonly Validate<ModifyWorkDesignCmd> validate;
        readonly IGuidGenerator _guidGenerator;
        readonly IUnitOfWork unitOfWork;

        public ModifyWorkDesignHandler(IProcessDesignRepo processDesignRepo, Validate<ModifyWorkDesignCmd> validate, IGuidGenerator guidGenerator, IUnitOfWork unitOfWork, IProcessNodeRepo processNodeRepo)
        {
            this.processDesignRepo = processDesignRepo;
            this.validate = validate;
            _guidGenerator = guidGenerator;
            this.unitOfWork = unitOfWork;
            this.processNodeRepo = processNodeRepo;
        }

        public async Task<bool> Handle(ModifyWorkDesignCmd request, CancellationToken cancellationToken)
        {

            await validate.ValidateAsync(request);
            var entity = await processDesignRepo.QueryAll()
                .Include(x => x.ProcessNodes)
                .ThenInclude(x => x.NodeApprovers)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (entity is null) throw new BusinessException(MessageType.Warn, "非法流程设计,无法执行更新操作!", "流程设计主键ID不存在!");
            entity.ChangeProcessDesign(
                request.ProcessName,
                request.IsEnable,
                request.DeptCode,
                request.processTempType,
                request.OrgCode,
                request.HospCode);
            request.workNodes.ForEach(item =>
            {
                if (string.IsNullOrWhiteSpace(item.Id))
                    item.SetId(_guidGenerator.Create().ToString());
            });

            List<ProcessNode> WorkNodes = new();
            foreach (var item in request.workNodes)
            {
                var currentIndex = request.workNodes.IndexOf(item);
                var workNode = entity.ProcessNodes.FirstOrDefault(x => x.Id == item.Id);
                if (workNode is null)
                {
                    var addWorkNode = new ProcessNode(item.Id, entity.Id, item.NodeName, $"{entity.ProcessCode}_{(currentIndex + 1).ToString("D2")}",
                   currentIndex == 0 ? Guid.Empty.ToString() : request.workNodes[currentIndex - 1].Id,
                   currentIndex == (request.workNodes.Count - 1) ? CustomConstant.EndGuId : request.workNodes[currentIndex + 1].Id,
                   string.Join(",", item.EventDirectionBranch),
                   item.IsRejectToNode,
                    currentIndex + 1,
                   item.CurrentNodeApprovers
                   .Select(x => new NodeApprover(_guidGenerator.Create().ToString(), item.Id, x.ApproverId, x.ApproverAccount, x.ApproverName)).ToList(),
                   item.NodeMapWorkflowStatus);
                    WorkNodes.Add(addWorkNode);
                    continue;
                }
                entity.ProcessNodes.FirstOrDefault(x => x.Id == item.Id)
                      ?.SetIsRejectToNode(item.IsRejectToNode)
                      ?.SetNodeCode($"{entity.ProcessCode}_{(currentIndex + 1).ToString("D2")}")
                      ?.SetLowerNodeId(currentIndex == (request.workNodes.Count - 1) ? CustomConstant.EndGuId : request.workNodes[currentIndex + 1].Id)
                      ?.SetUpperNodeId(currentIndex == 0 ? Guid.Empty.ToString() : request.workNodes[currentIndex - 1].Id)
                      ?.SetNodeApprovers(item.CurrentNodeApprovers
                      .Select(x => new NodeApprover(_guidGenerator.Create().ToString(), item.Id, x.ApproverId, x.ApproverAccount, x.ApproverName)).ToList())
                      ?.SetNodeName(item.NodeName)
                      ?.SetOderNo(currentIndex + 1)
                      ?.SetNodeMapWorkflowStatus(item.NodeMapWorkflowStatus);
            }
            entity.ProcessNodes.Where(x => !request.workNodes.Exists(_ => _.Id == x.Id)).ToList().ForEach(x =>
            {
                x.ChangeDelete();
                x.NodeApprovers.ToList().ForEach(_ => _.ChangeDelete());
            });
            processDesignRepo.Update(entity);
            await processNodeRepo.AddAsync(WorkNodes, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
