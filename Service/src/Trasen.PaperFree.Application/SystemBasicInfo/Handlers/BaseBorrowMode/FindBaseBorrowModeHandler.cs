using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.BaseBorrowMode;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.BaseBorrowMode;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.BaseBorrowMode
{
    internal sealed class FindBaseBorrowModeHandler : IRequestHandler<FindBaseBorrowModeByIdQry, BaseBorrowModeDto?>
    {
        private readonly IBaseBorrowModeRepo processDesignRepo;

        public FindBaseBorrowModeHandler(IBaseBorrowModeRepo processDesignRepo)
        {
            this.processDesignRepo = processDesignRepo;
        }

        public async Task<BaseBorrowModeDto?> Handle(FindBaseBorrowModeByIdQry request, CancellationToken cancellationToken)
        {
            return await processDesignRepo.QueryAll().AsNoTracking()
                .Select(_ => new BaseBorrowModeDto()
                {
                    Id = _.Id,

                    ModeName = _.ModeName,
                    DeptCode = _.DeptCode,
                    OrgCode = _.OrgCode,
                    HospCode = _.HospCode,
                    IsEnable = _.IsEnable,
                }).FirstOrDefaultAsync(_ => _.Id == request.Id);
        }
    }
}