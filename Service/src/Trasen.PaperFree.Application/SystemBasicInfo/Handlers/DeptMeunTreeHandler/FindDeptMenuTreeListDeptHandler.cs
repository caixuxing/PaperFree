using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.DeptMeunTree;
using Trasen.PaperFree.Application.SystemBasicInfo.Query.DeptMeunTree;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.DeptMeunTreeHandler
{
    internal class FindDeptMenuTreeListDeptHandler : IRequestHandler<FindDeptMenuTreeDeptQuery, List<DeptMenuTreeListDeptDto>>
    {
        private readonly IDeptMenuTreeRepo _deptMenuTreeRepo;
        private readonly IArchiverMeumRepo _archiverMeumRepo;
        private List<Domain.ArchiveRecord.Entity.ArchiverMeum> archiverMeums;

        public FindDeptMenuTreeListDeptHandler(IDeptMenuTreeRepo deptMenuTreeRepo, IArchiverMeumRepo archiverMeumRepo)
        {
            _deptMenuTreeRepo = deptMenuTreeRepo;
            _archiverMeumRepo = archiverMeumRepo;
        }

        public async Task<List<DeptMenuTreeListDeptDto>> Handle(FindDeptMenuTreeDeptQuery request, CancellationToken cancellationToken)
        {
            var list = _deptMenuTreeRepo.QueryAll().AsNoTracking().Select(x => new DeptMenuTreeListDeptDto
            {
                Id = x.Id,
                DeptId = x.DeptId,
                ParentId = x.ParentId,
                IsRequired = x.IsRequired,
                ArchiverMeumId = x.ArchiverMeumId,
                OrgCode = x.OrgCode,
                MenuName = string.Empty,
                HospCode = x.HospCode,
                InputCode = x.InputCode,
            });
            var list1 = await list.WhereIf(_ => _.OrgCode == request.OrgCode, !string.IsNullOrWhiteSpace(request.OrgCode))
            .WhereIf(_ => _.HospCode == request.HospCode, !string.IsNullOrWhiteSpace(request.HospCode))
             .WhereIf(_ => _.DeptId == request.DeptId, !string.IsNullOrWhiteSpace(request.DeptId))
             .ToListAsync();

            archiverMeums = _archiverMeumRepo.QueryAll().AsNoTracking().ToList();
            return BuildTree(list1, null);
        }

        public List<DeptMenuTreeListDeptDto> BuildTree(List<DeptMenuTreeListDeptDto> factoryModels, string? parentid)
        {
            var treeNodes = new List<DeptMenuTreeListDeptDto>();
            var joinedData = factoryModels.Join
            (
            archiverMeums,
            t1 => t1.ArchiverMeumId,
            t2 => t2.Id,
            (t1, t2) => new DeptMenuTreeListDeptDto
            {
                Id = t1.Id,
                ParentId = t1.ParentId,
                IsRequired = t1.IsRequired,
                ArchiverMeumId = t1.ArchiverMeumId,
                OrgCode = t1.OrgCode,
                MenuName = t2.MenuName,
                HospCode = t1.HospCode,
                InputCode = t1.InputCode,
                OrderbyId=t2.Orderby,
               
            }).OrderBy(x=>x.OrderbyId).ToList();
            foreach (var model in joinedData.Where(x => x.ParentId == parentid))
            {
                var treeNode = new DeptMenuTreeListDeptDto
                {
                    Id = model.Id,
                    ParentId = model.ParentId,
                    IsRequired = model.IsRequired,
                    ArchiverMeumId = model.ArchiverMeumId,
                    OrgCode = model.OrgCode,
                    MenuName = model.MenuName,
                    HospCode = model.HospCode,
                    InputCode = model.InputCode,
                    Children = BuildTree(joinedData, model.ArchiverMeumId)
                };
                treeNodes.Add(treeNode);
            }
            return treeNodes;
        }
    }
}