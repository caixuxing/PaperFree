using Microsoft.EntityFrameworkCore;
using System.Linq;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.DeptMeunTree;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.DeptMeunTreeHandler
{
    internal class ModifyDeptMenuTreeHandler : IRequestHandler<ModifyDeptMeunTreeCmdsListCmd, bool>
    {
        private readonly IDeptMenuTreeRepo deptMeumTreeRepo;
        private readonly Validate<ModifyDeptMeunTreeCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly IGuidGenerator guidGenerator;

        public ModifyDeptMenuTreeHandler(IDeptMenuTreeRepo deptMeumTreeRepo,
            IUnitOfWork unitOfWork,
            IGuidGenerator guidGenerator,
            Validate<ModifyDeptMeunTreeCmd> validate)
        {
            this.deptMeumTreeRepo = deptMeumTreeRepo;
            this.unitOfWork = unitOfWork;
            this.guidGenerator = guidGenerator;
            this.validate = validate;
        }

        public async Task<bool> Handle(ModifyDeptMeunTreeCmdsListCmd request, CancellationToken cancellationToken)
        {
            List<DeptMeMenuTreeEntity> insertList = new List<DeptMeMenuTreeEntity>();
            var ids = request.cmd.ListTreeData.Select(x => x.ArchiverMeumId);
            var entitys = await deptMeumTreeRepo.QueryAll()
                .Where(x => ids.Contains(x.ArchiverMeumId) && x.OrgCode == request.cmd.OrgCode && x.HospCode == request.cmd.HospCode&& request.cmd.DeptId.Contains(x.DeptId)).ToListAsync();
            var value = request.cmd.DeptId.Intersect(entitys.Select(x => x.DeptId).ToArray());
            var deptid = request.cmd.DeptId.Except(entitys.Select(x => x.DeptId).Distinct().ToArray());
                if (entitys is null) throw new BusinessException(MessageType.Error, "更新失败!", "必传文件配置实体不存在!");
     
                foreach (var item in entitys)
                {
                    var requs = request.cmd.ListTreeData.FirstOrDefault(x => x.ArchiverMeumId == item.ArchiverMeumId && value.Contains(item.DeptId));
                    if (requs == null)
                        continue;
                    item.UpadteMeumTree(requs.ArchiverMeumId, requs.IsRequired, requs.ParentId);
                }
            
            foreach (var itemdept in deptid)
            {
                var insertQuery= request.cmd.ListTreeData.Where(t => !entitys.Any(b => b.ArchiverMeumId == t.ArchiverMeumId && b.DeptId == itemdept))
                    .Select(x => new DeptMeMenuTreeEntity(guidGenerator.Create().ToString(), itemdept, x.ArchiverMeumId, x.ParentId, x.IsRequired, request.cmd.OrgCode, request.cmd.HospCode, request.cmd.InputCode));
                insertList.AddRange(insertQuery);
            }
            deptMeumTreeRepo.Update(entitys);
            await deptMeumTreeRepo.AddAsyncList(insertList.ToList(), cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}