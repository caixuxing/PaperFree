using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.Dto;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.ProcessRecord;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ProcessRecord
{
    internal class FindProcessNodeByProcessDesignIdHandler : IRequestHandler<FindProcessNodeByProcessDesignIdQry, IEnumerable<DropSelectDto<string>>>
    {
        private readonly IProcessNodeRepo processNodeRepo;

        public FindProcessNodeByProcessDesignIdHandler(IProcessNodeRepo processNodeRepo)
        {
            this.processNodeRepo = processNodeRepo;
        }

        public async Task<IEnumerable<DropSelectDto<string>>> Handle(FindProcessNodeByProcessDesignIdQry request, CancellationToken cancellationToken)
        {
            var data = await processNodeRepo.QueryAll().AsNoTracking()
                 .Where(x => x.ProcessDesignId == request.id)
                 .OrderBy(x => x.OderNo)
                 .Select(x => new DropSelectDto<string>
                 {
                     Id = x.Id,
                     Name = x.NodeName
                 })
                 .ToListAsync(cancellationToken);
            data.Insert(0, new DropSelectDto<string>() { Id = Guid.Empty.ToString(), Name = "开始" });
            data.Add(new DropSelectDto<string>() { Id = CustomConstant.EndGuId, Name = "结束" });
            return data;
        }
    }
}