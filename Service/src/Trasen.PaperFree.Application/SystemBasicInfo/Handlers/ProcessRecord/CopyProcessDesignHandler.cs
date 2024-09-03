using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ProcessRecord
{
    internal class CopyProcessDesignHandler : IRequestHandler<CopyProcessDesignCmd, bool>
    {
        private readonly IProcessDesignRepo processDesignRepo;
        private readonly IProcessNodeRepo processNodeRepo;
        private readonly Validate<CopyProcessDesignCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly IGuidGenerator _guidGenerator;

        public CopyProcessDesignHandler(IProcessDesignRepo processDesignRepo, Validate<CopyProcessDesignCmd> validate, IProcessNodeRepo processNodeRepo, IUnitOfWork unitOfWork, IGuidGenerator guidGenerator)
        {
            this.processDesignRepo = processDesignRepo;
            this.validate = validate;
            this.processNodeRepo = processNodeRepo;
            this.unitOfWork = unitOfWork;
            _guidGenerator = guidGenerator;
        }

        /// <summary>
        /// 复制流程模板处理器
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<bool> Handle(CopyProcessDesignCmd request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);
            var data = await processDesignRepo.QueryAll().AsNoTracking()
                  .Include(x => x.ProcessNodes)
                  .FirstOrDefaultAsync(x => x.Id == request.id);
            if (data is null)
                throw new BusinessException(MessageType.Warn, "没找到可复制的流程模板!");
            //初始化基础信息
            data.CreationTime = DateTime.Now;
            data.CreatorId = string.Empty;
            data.LastModifyId = string.Empty;
            data.LastModifyTime = null;
            data.Id = Guid.Empty.ToString();
            data.ChangeIsEnable(false);
            foreach (var item in data.ProcessNodes)
            {
                item.Id = _guidGenerator.Create().ToString();
                item.CreationTime = DateTime.Now;
                item.CreatorId = string.Empty;
                item.LastModifyId = string.Empty;
                item.LastModifyTime = DateTime.Now;
            }
            await processDesignRepo.AddAsync(data, cancellationToken);
            await processNodeRepo.AddAsync(data.ProcessNodes.ToList(), cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}