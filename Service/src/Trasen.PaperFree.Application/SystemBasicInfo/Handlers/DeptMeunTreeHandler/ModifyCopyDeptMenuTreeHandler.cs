using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.DeptMeunTree;
using Trasen.PaperFree.Domain.Base;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;
using static Slapper.AutoMapper;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.DeptMeunTreeHandler
{
    internal class ModifyCopyDeptMenuTreeHandler : IRequestHandler<CreateCopyDeptMeunTreeCmdsListCmd, bool>
    {
        private readonly IDeptMenuTreeRepo deptMeumTreeRepo;
        private readonly Validate<ModifyDeptMeunTreeCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly IGuidGenerator guidGenerator;

        public ModifyCopyDeptMenuTreeHandler(IDeptMenuTreeRepo deptMeumTreeRepo,
            IUnitOfWork unitOfWork,
            IGuidGenerator guidGenerator,
            Validate<ModifyDeptMeunTreeCmd> validate)
        {
            this.deptMeumTreeRepo = deptMeumTreeRepo;
            this.unitOfWork = unitOfWork;
            this.guidGenerator = guidGenerator;
            this.validate = validate;
        }
        public async Task<bool> Handle(CreateCopyDeptMeunTreeCmdsListCmd request, CancellationToken cancellationToken)
        {
            List<DeptMeMenuTreeEntity> insertList=new List<DeptMeMenuTreeEntity>();
           var ids = request.cmd.ListTreeData.Select(x => x.ArchiverMeumId);
            var dept = await deptMeumTreeRepo.QueryAll().Where(x => x.OrgCode == request.cmd.OrgCode&&x.HospCode==request.cmd.HospCode).Select(x=>x.DeptId).Distinct().ToListAsync();
            string[] numbersArray=dept.ToArray();
           var deptid= request.cmd.DeptId.Except(numbersArray);
            var entitys = await deptMeumTreeRepo.QueryAll().Where(x => ids.Contains(x.ArchiverMeumId)).ToListAsync();
            foreach (var item in deptid)
            {
                var List = request.cmd.ListTreeData.Where(t => !entitys.Any(b => b.ArchiverMeumId == t.ArchiverMeumId && b.DeptId == item))
                    .Select(x => new DeptMeMenuTreeEntity(guidGenerator.Create().ToString(), item, x.ArchiverMeumId, x.ParentId, x.IsRequired, request.cmd.OrgCode, request.cmd.HospCode, request.cmd.InputCode)).ToList();
                insertList.AddRange(List);
            }
            await deptMeumTreeRepo.AddAsyncList(insertList, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}