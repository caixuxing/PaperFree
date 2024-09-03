using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.ArchiverMeumDto;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.ArchiverMeumQry;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ArchiverMeumHandler
{
    internal class FindArchiverMeumHandler : IRequestHandler<FindArchiverMeumByIdQry, FindArchiverMeumDto?>
    {
        private readonly IArchiverMeumRepo _repo;

        public FindArchiverMeumHandler(IArchiverMeumRepo repo)
        {
            _repo = repo;
        }

        public async Task<FindArchiverMeumDto?> Handle(FindArchiverMeumByIdQry request, CancellationToken cancellationToken)
        {
            return await _repo.QueryAll().AsNoTracking()
                 .Select(x => new FindArchiverMeumDto()
                 {
                     Id = x.Id,
                     MenuName = x.MenuName,
                     ParentId = x.ParentId,
                     Permission = x.Permission,
                     MeumType = x.MeumType,
                     SecretLevel = x.SecretLevel,
                     IsAllorg = x.IsAllorg,
                     Orderby=x.Orderby,
                 }).FirstOrDefaultAsync(x => x.Id == request.id);
        }
    }
}