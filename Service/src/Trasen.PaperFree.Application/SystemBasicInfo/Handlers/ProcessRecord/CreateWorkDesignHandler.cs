using Microsoft.EntityFrameworkCore;
using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;
using Trasen.PaperFree.Domain.ProcessRecord.Entity;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using static Slapper.AutoMapper;
using Trasen.PaperFree.Domain.SeedWork;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ProcessRecord
{
    internal sealed class CreateWorkDesignHandler : IRequestHandler<CreateWorkDesignCmd, string>
    {

        readonly IProcessDesignRepo processDesignRepo;
         readonly Validate<CreateWorkDesignCmd> validate;
        readonly IGuidGenerator _guidGenerator;
         readonly IUnitOfWork unitOfWork;

        public CreateWorkDesignHandler(IProcessDesignRepo processDesignRepo,
            IProcessNodeRepo processNodeRepo,
            Validate<CreateWorkDesignCmd> validate,
            IGuidGenerator guidGenerator,
            IUnitOfWork unitOfWork)
        {
            this.processDesignRepo = processDesignRepo;
            this.validate = validate;
            _guidGenerator = guidGenerator;
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreateWorkDesignCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            //创建流程信息
            var model = await processDesignRepo.QueryAll().AsNoTracking().FirstOrDefaultAsync(_ => _.OrgCode == request.OrgCode &&
                _.HospCode == request.HospCode && _.DeptCode == request.DeptCode);
            int oldProcessCode = 1;
            if (model is not null)
            {
                oldProcessCode = (int.Parse(model.ProcessCode.Substring(model.ProcessCode.Length - 4)) + 1);
                if (request.IsEnable) throw new BusinessException(MessageType.Warn, "相同机构>>院区>>科室下类型流程模板请勿同时启用多份！");
            }
            string newProcessCode = string.Empty;
            request.ProcessName.ToList().ForEach(item => newProcessCode += new ChineseChar(item).Pinyins.FirstOrDefault()?.ToString().Substring(0, 1));
            var entity = new ProcessDesign(
                _guidGenerator.Create().ToString(),
                request.ProcessName,
                $"{newProcessCode}{oldProcessCode.ToString("D4")}",
                request.IsEnable,
                request.OrgCode,
                request.HospCode,
                request.processTempType,
                request.DeptCode);
            request.workNodes.ForEach(item => item.SetId(_guidGenerator.Create().ToString()));
            request.workNodes.ForEach((item) =>
            {
                var currentIndex = request.workNodes.IndexOf(item);
                var processNodeEntity = new ProcessNode(item.Id,
                    entity.Id,
                    item.NodeName,
                    $"{entity.ProcessCode}_{(currentIndex+1).ToString("D2")}",
                    currentIndex == 0 ? Guid.Empty.ToString() : request.workNodes[currentIndex - 1].Id,
                    currentIndex == (request.workNodes.Count-1) ? CustomConstant.EndGuId : request.workNodes[currentIndex + 1].Id,
                    string.Join(",", item.EventDirectionBranch),
                    item.IsRejectToNode,
                    currentIndex+1,
                    item.CurrentNodeApprovers
                    .Select(x => new NodeApprover(_guidGenerator.Create().ToString(), item.Id, x.ApproverId, x.ApproverAccount, x.ApproverName)).ToList(),
                    item.NodeMapWorkflowStatus);
                entity.SetProcessNodes(processNodeEntity);
            });
            await processDesignRepo.AddAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
