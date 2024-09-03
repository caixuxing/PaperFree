using Microsoft.EntityFrameworkCore;
using Microsoft.International.Converters.PinYinConverter;
using System.Text.RegularExpressions;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;
using Trasen.PaperFree.Domain.ProcessRecord.Entity;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ProcessRecord
{
    internal sealed class CreateProcessDesignHandler : IRequestHandler<CreateProcessDesignCmd, string>
    {
        private readonly IProcessDesignRepo processDesignRepo;
        private readonly Validate<CreateProcessDesignCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUser currentUser;

        public CreateProcessDesignHandler(IProcessDesignRepo processDesignRepo,
            Validate<CreateProcessDesignCmd> validate, IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            this.processDesignRepo = processDesignRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            this.currentUser = currentUser;
        }

        public async Task<string> Handle(CreateProcessDesignCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);

            var model = await processDesignRepo.QueryAll().AsNoTracking()
                 .FirstOrDefaultAsync(_ => _.OrgCode == request.OrgCode &&
                 _.HospCode == request.HospCode &&
                 _.DeptCode == request.DeptCode);
            int oldProcessCode = 1;
            if (model is not null)
            {
                oldProcessCode = (int.Parse(model.ProcessCode.Substring(model.ProcessCode.Length - 4)) + 1);
                if (request.IsEnable)
                    throw new BusinessException(MessageType.Warn, "相同机构>>院区>>科室下类型流程模板请勿同时启用多份！");
            }
            string newProcessCode = string.Empty;
            //取流程名称首字母
            request.ProcessName.ToList().ForEach(item => newProcessCode += new ChineseChar(item).Pinyins.FirstOrDefault()?.ToString().Substring(0, 1));
            var entity = new ProcessDesign(
                Guid.Empty.ToString(),
                request.ProcessName,
                $"{newProcessCode}{oldProcessCode.ToString("D4")}",
                request.IsEnable,
                request.OrgCode,
                request.HospCode,
                request.processTempType,
                request.DeptCode);
            await processDesignRepo.AddAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            return entity.Id;
        }
    }
}