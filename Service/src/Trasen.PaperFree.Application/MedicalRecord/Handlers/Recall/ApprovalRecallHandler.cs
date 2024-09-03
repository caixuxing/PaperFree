using Microsoft.EntityFrameworkCore;
using Trasen.PaperFree.Application.MedicalRecord.Commands.Recall;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;
using Trasen.PaperFree.Domain.IgnoreItme.Repository;
using Trasen.PaperFree.Domain.PatientDetails.Repository;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using Trasen.PaperFree.Domain.RecallRecord.DomainService;
using Trasen.PaperFree.Domain.RecallRecord.Entity;
using Trasen.PaperFree.Domain.RecallRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.Recall;

/// <summary>
/// 审批-召回申请处理器
/// </summary>
internal class ApprovalRecallHandler : IRequestHandler<ApprovalRecallCmd, bool>
{
     readonly Validate<ApprovalRecallCmd> validate;
     readonly ICurrentUser currentUser;
     readonly IRecallApplyRepo recallApplyRepo;
     readonly IUnitOfWork unitOfWork;
     readonly IProcessDesignRepo processDesignRepo;
     readonly IOutpatientInfoRepo outpatientInfoRepo;
     readonly IGuidGenerator guidGenerator;
     readonly IRecallRecordService recallRecordService;
    readonly IDeptMenuTreeRepo deptMenuTreeRepo;
    readonly IArchiverMeumRepo archiverMeumRepo;
    readonly IFilesHisRepo filesHisRepo;
    readonly IFilesOtherRepo filesOtherRepo;
    readonly IIgnoreItmeRepo ignoreItmeRepo;

    public ApprovalRecallHandler(
        Validate<ApprovalRecallCmd> validate,
        ICurrentUser currentUser,
        IRecallApplyRepo recallApplyRepo,
        IUnitOfWork unitOfWork,
        IProcessDesignRepo processDesignRepo,
        IOutpatientInfoRepo outpatientInfoRepo,
        IGuidGenerator guidGenerator,
        IRecallRecordService recallRecordService,
        IDeptMenuTreeRepo deptMenuTreeRepo,
        IArchiverMeumRepo archiverMeumRepo,
        IFilesHisRepo filesHisRepo,
        IFilesOtherRepo filesOtherRepo,
        IIgnoreItmeRepo ignoreItmeRepo)
    {
        this.validate = validate;
        this.currentUser = currentUser;
        this.recallApplyRepo = recallApplyRepo;
        this.unitOfWork = unitOfWork;
        this.processDesignRepo = processDesignRepo;
        this.outpatientInfoRepo = outpatientInfoRepo;
        this.guidGenerator = guidGenerator;
        this.recallRecordService = recallRecordService;
        this.deptMenuTreeRepo = deptMenuTreeRepo;
        this.archiverMeumRepo = archiverMeumRepo;
        this.filesHisRepo = filesHisRepo;
        this.filesOtherRepo = filesOtherRepo;
        this.ignoreItmeRepo = ignoreItmeRepo;
    }

    public async Task<bool> Handle(ApprovalRecallCmd request, CancellationToken cancellationToken)
    {
        //校验请求参数
        await validate.ValidateAsync(request);

        //申请归档信息
        var recallApply = await recallApplyRepo.FindById(request.RecallApplyId);
        if (recallApply is null)
            throw new BusinessException(MessageType.Warn, "非法召回申请信息,审批失败！");
        if (recallApply.CurrentStatus == ProcessStatusType.END)
            throw new BusinessException(MessageType.Warn, "流程已结束,请勿重复审批！");

        //流程模板信息
        var processDesign = await processDesignRepo.QueryAll().AsTracking()
            .Include(x => x.ProcessNodes)
            .ThenInclude(node => node.NodeApprovers)
            .SingleOrDefaultAsync(x => x.Id == recallApply.ProcessDesignId);

        if (processDesign is null)
            throw new BusinessException(MessageType.Warn, "流程模板缺失无法进行审批操作!");




        //流程事件状态流
        var medicalRecordStatus = recallRecordService.ProcessEnventStatuFlow(
            request.IsRejectToNode ?? false,
            request.RejectNodeId ?? string.Empty,
            request.ApprovalResult,
            processDesign,
            recallApply);
        //更新归档申请
        recallApplyRepo.Update(recallApply);

        //出院信息
        var outpatientInfo = await outpatientInfoRepo.FindById(recallApply.ArchiveId);
        if (outpatientInfo is null)
            throw new BusinessException(MessageType.Warn, "病历信息缺失无法进行审批操作!");

        if(outpatientInfo.Status== WorkFlowState.AWAITCOMMIT&& request.ApprovalResult== EventDirectionType.PASS)
        await CheckMenuRequiredItem(outpatientInfo.OrgCode, outpatientInfo.HospCode ?? string.Empty, outpatientInfo.OutDeptCode, outpatientInfo.ArchiveId);

        //出院信息状态更改
        outpatientInfo.ChnageStatus(medicalRecordStatus);
        outpatientInfoRepo.Update(outpatientInfo);

        //召回申请-审批信息
        await recallApplyRepo.AddAsync(new RecallApprover(
            guidGenerator.Create().ToString(),
            request.RecallApplyId,
            WorkFlowState.AWAITCOMMIT,
            request.ApprovalResult,
            request.ApprovalRemark,
            currentUser.Id,
            DateTime.Now),cancellationToken);
        //保存
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }



    /// <summary>
    /// 校验菜单必传传文件项
    /// </summary>
    /// <returns></returns>
    private async Task CheckMenuRequiredItem(string orgCode, string hospCode, string outDeptCode, string archiveId)
    {
        //查询病历出院科室目录及必填项
        var archiverMenuList = await deptMenuTreeRepo.QueryAll().AsTracking()
            .GroupJoin(archiverMeumRepo.QueryAll().AsNoTracking(), a => a.ArchiverMeumId, b => b.Id, (n, m) => new
            {
                n.Id,
                n.OrgCode,
                n.HospCode,
                n.DeptId,
                n.ArchiverMeumId,
                m,
                n.IsRequired
            })
            .SelectMany(x => x.m, (n, m) => new
            {
                n.Id,
                n.OrgCode,
                n.HospCode,
                n.DeptId,
                MenuId = n.ArchiverMeumId,
                MenuName = m.MenuName,
                n.IsRequired
            })
            .Where(x => x.DeptId == outDeptCode && x.OrgCode == orgCode && x.HospCode == hospCode)
            .ToListAsync();
        if (archiverMenuList is null || !archiverMenuList.Any())
            throw new BusinessException(MessageType.Warn, $"病历不完整,缺少病历目录信息！");
        //收集必传项Id
        var isRequiredIds = archiverMenuList.Where(x => x.IsRequired == WhetherType.YES).Select(x => x.MenuId).ToList();
        var fileData = await filesHisRepo.QueryAll().AsNoTracking()
                 .Select(x => new { x.MeumId, x.ArchiveId })
                 .Where(x => isRequiredIds.Contains(x.MeumId) && x.ArchiveId == archiveId)
                 .ToListAsync();
        var fileOther = await filesOtherRepo.QueryAll().AsNoTracking()
            .Select(x => new { MeumId = x.MenuId, x.ArchiveId })
                 .Where(x => isRequiredIds.Contains(x.MeumId) && x.ArchiveId == archiveId)
                 .ToListAsync();
        fileData.AddRange(fileOther);
        List<(string, string)> errorMsg = new List<(string, string)>();
        archiverMenuList.Select(x => new { x.Id, x.MenuId, x.IsRequired, x.MenuName, FileCount = fileData.Count(c => c.MeumId == x.MenuId) })
        .ToList()
        .ForEach(item =>
            {
                if (item.IsRequired == WhetherType.YES && item.FileCount < 1)
                    errorMsg.Add((item.MenuName, item.Id));
            });
        if (errorMsg.Any())
        {
            //忽略项
            var ignoreList = await ignoreItmeRepo.QueryAll()
                  .Select(x => new { x.ArchiveId, x.MeumTreeId })
                  .Where(x => x.ArchiveId == archiveId).AsNoTracking().ToListAsync();
            var data = errorMsg.Where(x => !ignoreList.Exists(t => t.MeumTreeId == x.Item2)).ToList();
            if (data.Any())
                throw new BusinessException(MessageType.Warn, "目录必传文件缺少", "目录必传文件缺少", data.Select(x => x.Item1).ToList(), ResultCode.PARAM_ERROR, true);
        }
    }
}