using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Http;
using Trasen.PaperFree.Application.MedicalRecord.Dto.Archive;
using Trasen.PaperFree.Application.MedicalRecord.Query.Archive;
using Trasen.PaperFree.Application.SystemBasicInfo.Common;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Const;
using Trasen.PaperFree.Domain.Shared.Extend;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.Archive
{
    internal class FindArchiveApplyPageListHandler : IRequestHandler<FindArchiveApplyPageListQry, PageData<List<ArchiveApplyPageDto>?>>
    {
        private readonly IArchiveApplyRepo archiveApplyRepo;
        private readonly IOutpatientInfoRepo outpatientInfoRepo;
        private readonly IProcessDesignRepo processDesignRepo;
        private readonly Validate<FindArchiveApplyPageListQry> validate;
        private readonly IHttpClientFactory httpClientFactory;

        public FindArchiveApplyPageListHandler(IArchiveApplyRepo archiveApplyRepo, IOutpatientInfoRepo outpatientInfoRepo, IProcessDesignRepo processDesignRepo, Validate<FindArchiveApplyPageListQry> validate, IHttpClientFactory httpClientFactory)
        {
            this.archiveApplyRepo = archiveApplyRepo;
            this.outpatientInfoRepo = outpatientInfoRepo;
            this.processDesignRepo = processDesignRepo;
            this.validate = validate;
            this.httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 归档申请分页列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PageData<List<ArchiveApplyPageDto>?>> Handle(FindArchiveApplyPageListQry request, CancellationToken cancellationToken)
        {
            await validate.ValidateAsync(request);

            var query = archiveApplyRepo.QueryAll().GroupJoin(outpatientInfoRepo.QueryAll(), a => a.ArchiveId, b => b.ArchiveId, (n, m) => new
            {
                n.Id,
                n.ArchiveId,
                n.ArchiveName,
                n.ProcessDesignId,
                n.CurrentApprovalNodeId,
                n.CurrentStatus,
                n.CreationTime,
                n.CreatorId,
                n.IsEnd,
                n.OrgCode,
                n.HospCode,
                n.ApplyPersonName,
                n.NodeApprovaName,
                m
            })
                .SelectMany(x => x.m.DefaultIfEmpty(), (n, m) => new
                {
                    Id = n.Id,
                    ArchiveName = n.ArchiveName,
                    CreationTime = n.CreationTime,
                    CurrentApprovalNodeId = n.CurrentApprovalNodeId ?? string.Empty,
                    CurrentStatus = n.CurrentStatus,
                    IsEnd = n.IsEnd,
                    ProcessDesignId = n.ProcessDesignId,
                    OrgCode = n.OrgCode,
                    HospCode = n.HospCode,
                    CurrentNodeApprovalPerson = n.NodeApprovaName ?? string.Empty,
                    CreatorName = n.ApplyPersonName,
                    m.AdmissId,
                    m.OutDeptCode,
                    m.Name
                })
                   .WhereIf(x => x.IsEnd == request.IsEnd, !(request.IsEnd is null))
                   .WhereIf(x => x.CreationTime >= request.StartTime, !(request.StartTime is null))
                   .WhereIf(x => x.CreationTime <= request.EndTime, !(request.EndTime is null))
                   .WhereIf(x => x.CurrentStatus == request.CurrentStatus, !(request.CurrentStatus is null))
                   .WhereIf(x => x.OrgCode == request.OrgCode, !string.IsNullOrWhiteSpace(request.OrgCode))
                    .WhereIf(x => x.HospCode == request.HospCode, !string.IsNullOrWhiteSpace(request.HospCode))
                    .WhereIf(x => x.OutDeptCode == request.DeptId, !string.IsNullOrWhiteSpace(request.DeptId))
                     .WhereIf(x => EF.Functions.Like(x.Name, $"%{request.Name}%"), !string.IsNullOrWhiteSpace(request.Name))
                     .WhereIf(x => EF.Functions.Like(x.AdmissId, $"{request.AdmissId}%"), !string.IsNullOrWhiteSpace(request.AdmissId))
                   .AsNoTracking();
            var total = await query.CountAsync(cancellationToken);
            var data = await query
                .OrderBy(x => x.CurrentStatus)
                .ThenByDescending(x => x.CreationTime)
                .Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken);
            //收集流程模板ID
            var ProcessDesignIds = data.Select(x => x.ProcessDesignId).ToList();
            var processDesigns = await processDesignRepo.QueryAll().Where(x => ProcessDesignIds.Contains(x.Id))
                .Include(x => x.ProcessNodes)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            var nodeList = processDesigns.SelectMany(x => x.ProcessNodes).ToList();

            var baseDataOrg = await httpClientFactory.PostAsync<List<FindOrgDto>>(TrasenApiConst.Org, new
            {
                ORG_CODE = request.OrgCode,
                IS_DELETE = "N",
                PageIndex = 1,
                PageSize = 9999
            });
            var deptList = await httpClientFactory.PostAsync<List<FindOrgDepartmentDto>>(TrasenApiConst.OrgDepartment, new
            {
                pageIndex = 1,
                pageSize = 9999,
                ORG_CODE = request.OrgCode,
                HOSP_CODE = request.HospCode,
                IS_DELETE = (char)YesOrNoType.NO
            });




            List<ArchiveApplyPageDto> list = new List<ArchiveApplyPageDto>();
            data.ForEach(item =>
            {
                var ListItem = new ArchiveApplyPageDto();
                ListItem.Id = item.Id;
                ListItem.ArchiveName = item.ArchiveName;
                var node = nodeList.FirstOrDefault(x => x.Id == item.CurrentApprovalNodeId);
                ListItem.CurrentApprovalNodeName = node?.NodeName ?? string.Empty;
                ListItem.CurrentNodeApprovalPerson = item.CurrentNodeApprovalPerson;
                var NextApprovalNodeId = node?.LowerNodeId ?? string.Empty;
                if (NextApprovalNodeId.Equals(CustomConstant.EndGuId))
                    ListItem.NextApprovalNodeName = "结束";
                else
                    ListItem.NextApprovalNodeName = nodeList.FirstOrDefault(x => x.Id == NextApprovalNodeId)?.NodeName ?? string.Empty;
                ListItem.CurrentStatusName = item.CurrentStatus.ToDescription();
                ListItem.CurrentStatus = item.CurrentStatus;
                ListItem.CreationTime = item.CreationTime;
                ListItem.CreatorName = item.CreatorName;
                ListItem.IsEnd = item.IsEnd;
                if (baseDataOrg is not null && !string.IsNullOrWhiteSpace(item.OrgCode))
                    ListItem.OrgName = baseDataOrg.FirstOrDefault(x => x.ORG_CODE == item.OrgCode && x.ORG_TYPE == "0")?.ORG_NAME ?? string.Empty;
                if (baseDataOrg is not null && !string.IsNullOrWhiteSpace(item.HospCode))
                    ListItem.HospName = baseDataOrg.FirstOrDefault(x => x.ORG_CODE == item.HospCode && x.ORG_TYPE == "1")?.ORG_NAME ?? string.Empty;
                ListItem.DeptName = deptList?.FirstOrDefault(x => x.DEPT_ID == item.OutDeptCode)?.DEPT_NAME ?? string.Empty;
                ListItem.Name = item.Name;
                ListItem.AdmissId = item.AdmissId;
                list.Add(ListItem);
            });
            var newList = list.Where(_ => new List<ProcessStatusType>() { ProcessStatusType.AWAITAPPROVAL, ProcessStatusType.UNDERAPPROVAL }.Contains(_.CurrentStatus)).ToList();
            newList.AddRange(list.Where(_ => !new List<ProcessStatusType>() { ProcessStatusType.AWAITAPPROVAL, ProcessStatusType.UNDERAPPROVAL }.Contains(_.CurrentStatus)).OrderByDescending(x=>x.CreationTime).ToList());
            return new(total, request.PageSize, request.PageIndex, newList);
        }
    }
}