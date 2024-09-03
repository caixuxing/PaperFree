using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.ArchiverMeumDto;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.ArchiverMeumQry;
using Trasen.PaperFree.Domain.ArchiveRecord.Entity;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Extend;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.ArchiverMeumHandler
{
    internal class FindArchiverMeumListHandler : IRequestHandler<FindArchiverMeumListQry, List<ArchiverMeumListDto>?>
    {
        private readonly IArchiverMeumRepo _repo;

        public FindArchiverMeumListHandler(IArchiverMeumRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<ArchiverMeumListDto>?> Handle(FindArchiverMeumListQry request, CancellationToken cancellationToken)
        {
            var list = await _repo.QueryAll()
                    .WhereIf(x => x.OrgCode == request.OrgCode, !string.IsNullOrWhiteSpace(request.OrgCode))
                    .WhereIf(x => x.HospCode == request.HospCode, !string.IsNullOrWhiteSpace(request.HospCode)).AsNoTracking()
                    .ToListAsync();

            return BuildTree(list.OrderBy(x => x.Orderby).ToList(), null);
        }

        public List<ArchiverMeumListDto> BuildTree(List<ArchiverMeum> factoryModels, string? parentId)
        {
            var treeNodes = new List<ArchiverMeumListDto>();
            foreach (var factoryModel in factoryModels.Where(x => x.ParentId == parentId))
            {
                var treeNode = new ArchiverMeumListDto
                {
                    ID = factoryModel.Id,
                    ParentId = factoryModel.ParentId,
                    MenuName = factoryModel.MenuName,
                    Permission = factoryModel.Permission,
                    MeumType = factoryModel.MeumType,
                    Orderby = factoryModel.Orderby,
                    SecretLevel = factoryModel.SecretLevel,
                    IsAllorg = factoryModel.IsAllorg,
                    IsHighShots = factoryModel.IsHighShots,
                    IsSignature = factoryModel.IsSignature,

                    Children = BuildTree(factoryModels, factoryModel.Id)
                };
                treeNodes.Add(treeNode);
            }
            return treeNodes;
        }
    }
}