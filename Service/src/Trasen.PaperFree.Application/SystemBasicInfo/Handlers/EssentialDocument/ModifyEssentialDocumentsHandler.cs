using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.EssentialDocument;
using Trasen.PaperFree.Domain.SystemBasicData.Entity;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Handlers.EssentialDocument
{
    internal sealed class ModifyEssentialDocumentsListHandler : IRequestHandler<ModifyEssentialDocumentsListCmd, bool>
    {
        private readonly IEssentialDocumentsRepo iessentialDocumentsRepo;
        private readonly Validate<ModifyEssentialDocumentsCmd> validate;
        private readonly IUnitOfWork unitOfWork;
        private readonly IGuidGenerator guidGenerator;

        public ModifyEssentialDocumentsListHandler(IEssentialDocumentsRepo iessentialDocumentsRepo, Validate<ModifyEssentialDocumentsCmd> validate, IUnitOfWork unitOfWork, IGuidGenerator guidGenerator)
        {
            this.iessentialDocumentsRepo = iessentialDocumentsRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            this.guidGenerator = guidGenerator;
        }

        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="request">入参</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<bool> Handle(ModifyEssentialDocumentsListCmd request, CancellationToken cancellationToken)
        {
            var ids = request.cmd.ListTreeDate.Select(x => x.Id);
            var entitys = await iessentialDocumentsRepo.QueryAll().Where(x => ids.Contains(x.Id)).ToListAsync();
            var insert = new List<EssentialDocuments>();

            if (entitys is null) throw new BusinessException(MessageType.Error, "更新失败!", "必传文件配置实体不存在!");
            foreach (var item in entitys)
            {
                var requs = request.cmd.ListTreeDate.FirstOrDefault(x => x.Id == item.Id);
                if (requs == null)
                    continue;
                if (requs.ChckeType == false)
                {
                    item.ChangeDelete();
                }
                else
                {
                    item.UpadteEssentialDocuments(requs.FatherMeumid, request.cmd.MeumType, request.cmd.Status, request.cmd.OrderId);
                }
            }
            //新增
            var insertList = request.cmd.ListTreeDate.Where(t => !entitys.Any(b => b.Id == t.Id))
                .Select(x => new EssentialDocuments(guidGenerator.Create().ToString(), request.cmd.DeptCode, x.FatherMeumid, request.cmd.MeumType, "0", request.cmd.OrderId, request.cmd.OrgCode, request.cmd.HospCode, request.cmd.InputCode));

            iessentialDocumentsRepo.Update(entitys);
            await iessentialDocumentsRepo.AddAsyncList(insertList.ToList(), cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

    /// <summary>
    /// 必传文件配置
    /// </summary>
    internal class ModifyEssentialDocumentsHandler : IRequestHandler<ModifyEssentialDocumentsCmd, bool>
    {
        private readonly IEssentialDocumentsRepo iessentialDocumentsRepo;
        private readonly Validate<ModifyEssentialDocumentsCmd> validate;
        private readonly IUnitOfWork unitOfWork;

        public ModifyEssentialDocumentsHandler(
            IEssentialDocumentsRepo essentialDocumentsRepo,
            Validate<ModifyEssentialDocumentsCmd> validate,
            IUnitOfWork unitOfWork)
        {
            this.iessentialDocumentsRepo = essentialDocumentsRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ModifyEssentialDocumentsCmd request, CancellationToken cancellationToken)
        {
            //await validate.ValidateAsync(request);
            //var entity = await iessentialDocumentsRepo.FindById(ids.Id);
            //if (entity is null) throw new BusinessException(MessageType.Error, "更新失败!", "必传文件配置实体不存在!");
            //entity.UpadteEssentialDocuments(request.DeptCode, request.FatherMeumid, request.MeumType, request.Status, request.OrderId);
            //iessentialDocumentsRepo.Update(entity);
            //await unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}