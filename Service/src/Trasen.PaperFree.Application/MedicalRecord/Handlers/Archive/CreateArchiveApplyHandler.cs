using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Cms;
using System.Linq;
using Trasen.PaperFree.Application.MedicalRecord.Commands.Archive;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Domain.ArchiveRecord.Entity;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;
using Trasen.PaperFree.Domain.IgnoreItme.Repository;
using Trasen.PaperFree.Domain.PatientDetails.Repository;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using Trasen.PaperFree.Domain.RecallRecord.Repository;
using Trasen.PaperFree.Domain.SeedWork;
using Trasen.PaperFree.Domain.Shared.Appsettings;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.Shared.HtppTools;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.Archive
{
    internal sealed class CreateArchiveApplyHandler : IRequestHandler<CreateArchiveApplyCmd, bool>
    {
        readonly IArchiveApplyRepo archiveApplyRepo;
        readonly Validate<CreateArchiveApplyCmd> validate;
        readonly IUnitOfWork unitOfWork;
        readonly IProcessDesignRepo processDesignRepo;
        readonly IOutpatientInfoRepo outpatientInfoRepo;
        readonly ICurrentUser currentUser;
        readonly IArchiverMeumRepo archiverMeumRepo;
        readonly IDeptMenuTreeRepo deptMenuTreeRepo;
        readonly IFilesHisRepo filesHisRepo;
        readonly IFilesOtherRepo filesOtherRepo;
        readonly IIgnoreItmeRepo ignoreItmeRepo;
        readonly IRecallApplyRepo recallApplyRepo;
        readonly IHttpClientFactory _httpClientFactory;

        public CreateArchiveApplyHandler(
            IArchiveApplyRepo archiveApplyRepo,
            Validate<CreateArchiveApplyCmd> validate,
            IUnitOfWork unitOfWork,
            IProcessDesignRepo processDesignRepo,
            IOutpatientInfoRepo outpatientInfoRepo,
            ICurrentUser currentUser,
            IArchiverMeumRepo archiverMeumRepo,
            IDeptMenuTreeRepo deptMenuTreeRepo,
            IFilesHisRepo filesHisRepo,
            IFilesOtherRepo filesOtherRepo,
            IIgnoreItmeRepo ignoreItmeRepo,
            IRecallApplyRepo recallApplyRepo,
            IHttpClientFactory httpClientFactory)
        {
            this.archiveApplyRepo = archiveApplyRepo;
            this.validate = validate;
            this.unitOfWork = unitOfWork;
            this.processDesignRepo = processDesignRepo;
            this.outpatientInfoRepo = outpatientInfoRepo;
            this.currentUser = currentUser;
            this.archiverMeumRepo = archiverMeumRepo;
            this.deptMenuTreeRepo = deptMenuTreeRepo;
            this.filesHisRepo = filesHisRepo;
            this.filesOtherRepo = filesOtherRepo;
            this.ignoreItmeRepo = ignoreItmeRepo;
            this.recallApplyRepo = recallApplyRepo;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> Handle(CreateArchiveApplyCmd request, CancellationToken cancellationToken)
        {
            //验证入参
            await validate.ValidateAsync(request);
            //读取病历信息
            var outpatientInfo = await outpatientInfoRepo.QueryAll().SingleOrDefaultAsync(x => x.ArchiveId == request.ArchivalId);
            if (outpatientInfo is null)
                throw new BusinessException(MessageType.Warn, "非法病历信息,操作失败！");

            //验证病历是否可发起归档申请
            if (outpatientInfo.Status != WorkFlowState.AWAITCOMMIT)
                throw new BusinessException(MessageType.Warn, $"病历状态【{outpatientInfo.Status.ToDescription()}】,无法发起归档操作！");

            await CheckMenuRequiredItem(outpatientInfo.OrgCode, outpatientInfo.HospCode ?? string.Empty, outpatientInfo.OutDeptCode, outpatientInfo.ArchiveId);

            //验证当前归档流程是否结束
            if (await archiveApplyRepo.QueryAll().AsNoTracking().AnyAsync(x => x.ArchiveId == request.ArchivalId && x.IsEnd == false))
                throw new BusinessException(MessageType.Warn, $"归档【{outpatientInfo.Name}:{outpatientInfo.AdmissId}】流程未结束,请勿重复提交申请");
            //读取流程模板
            var model = await processDesignRepo.QueryAll().AsNoTracking()
                .Include(x => x.ProcessNodes.OrderBy(x => x.OderNo))
                .ThenInclude(node => node.NodeApprovers)
                .FirstOrDefaultAsync(x => x.IsEnable == true &&
                                            x.DeptCode == outpatientInfo.OutDeptCode &&
                                            x.ProcessTempType == ProcessTempType.ARCHIVE &&
                                            x.OrgCode == outpatientInfo.OrgCode &&
                                            x.HospCode == outpatientInfo.HospCode);
            if (model is null || !model.ProcessNodes.Any())
                throw new BusinessException(MessageType.Warn, "没有找到出院科室的流程模板,请先设置流程归档流程模板！");

            var nodeModel = model.ProcessNodes.OrderBy(x => x.OderNo).FirstOrDefault();

            //创建归档申请
            var entity = new ArchiveApply(
                request.ArchivalId,
                $"归档【{outpatientInfo.Name}:{outpatientInfo.AdmissId}】",
                ProcessStatusType.AWAITAPPROVAL,
                false,
                model.Id,
                nodeModel!.Id,
                outpatientInfo.OrgCode,
                outpatientInfo.HospCode ?? string.Empty,
                nodeModel.NodeName,
                string.Join(",", nodeModel.NodeApprovers.Select(x => $"{x.ApproverAccount}【{x.ApproverName}】").ToList()),
                currentUser.UserNickName);

            await archiveApplyRepo.AddAsync(entity, cancellationToken);
            //改变病历状态已提交
            outpatientInfo.ChnageStatus(WorkFlowState.ALREADYCOMMIT);
            outpatientInfoRepo.Update(outpatientInfo);

            //判断当前病历是否在召回中
            if (await recallApplyRepo.QueryAll().AsNoTracking().AnyAsync(x => x.ArchiveId == request.ArchivalId && x.IsEnd == false))
                throw new BusinessException(MessageType.Warn, "当前病历正在召回中,无法发起归档申请操作！");
            //保存
            await unitOfWork.SaveChangesAsync(cancellationToken);

            //同步数据到病案示踪系统 【TODO: 待病案示踪系统接口完成后启用】
            var flag = true;
            if (!flag) {
                var CreateSyncDataParam = new {
                    signId=entity.Id.ToString(),
                    patientId=outpatientInfo.PatientId,
                    visitId=outpatientInfo.VisitId.ToString(),
                    admissId=outpatientInfo.AdmissId,
                    name=outpatientInfo.Name,
                    age=outpatientInfo.Age==null?"0":outpatientInfo.Age.ToString(),
                    idCard = outpatientInfo.IdCard??string.Empty,
                    outDept = outpatientInfo.OutDeptCode,
                    outDeptName = "",
                    enterDate = outpatientInfo.EnterDate.ToString("yyyy-MM-dd hh:mm:ss"),
                    outDate = outpatientInfo.OutDate.ToString("yyyy-MM-dd hh:mm:ss"),
                    doctorZzys = outpatientInfo.DoctorZzysCode, //主治医生
                    docotorZrys = outpatientInfo.DoctorZrysCode??string.Empty, //主任医师
                    docotorZyys = outpatientInfo.DoctorZyysCode,//住院医生
                    diseaseIcd = "",
                    diseaseName = "",
                    operateIcd = "",
                    operateName = "",
                    relationName = "",
                    relationTell = "",
                    archiveType ="1", //1、电子 2、纸质 3、缩微
                    status = "",
                };
                BackgroundJob.Enqueue(() => syncData(CreateSyncDataParam));
            }
            return true;
        }

        /// <summary>
        /// 归档申请信息提交到病案示踪系统
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [Queue("default")]
        [DisableConcurrentExecution(timeoutInSeconds: 180)]
        [AutomaticRetry(Attempts = 3, DelaysInSeconds = new[] { 100, 200, 300 })]
        public async Task syncData(object cmd)
        {
            var basePlatformSetting = Appsetting.Instance.GetSection("TRASEN_BASE_PLATFORM").Get<TrasenBasePlatformSetting>();
            await _httpClientFactory.PostAsync<dynamic>(Path.Combine(basePlatformSetting.MedicalDomainName, MedicalApiConst.syncData), cmd);
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
                    MenuName=(m==null?"":m.MenuName),
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
            List<(string,string)> errorMsg = new List<(string,string)>();
            archiverMenuList.Select(x => new {x.Id,x.MenuId, x.IsRequired, x.MenuName, FileCount = fileData.Count(c => c.MeumId == x.MenuId) })
                .ToList()
                .ForEach(item =>
                {
                    if (item.IsRequired== WhetherType.YES && item.FileCount < 1)
                        errorMsg.Add((item.MenuName,item.Id));
                });
            if (errorMsg.Any())
            {
                //忽略项
                var ignoreList= await ignoreItmeRepo.QueryAll()
                      .Select(x => new { x.ArchiveId, x.MeumTreeId })
                      .Where(x => x.ArchiveId == archiveId).AsNoTracking().ToListAsync();
               var data= errorMsg.Where(x => !ignoreList.Exists(t => t.MeumTreeId == x.Item2)).ToList();
                if(data.Any())
                throw new BusinessException(MessageType.Warn, "目录必传文件缺少", "目录必传文件缺少", data.Select(x=>x.Item1).ToList(), ResultCode.PARAM_ERROR,true);
            }
        }
    }
}