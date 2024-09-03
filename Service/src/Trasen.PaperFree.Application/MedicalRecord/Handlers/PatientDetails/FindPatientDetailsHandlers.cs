using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Security.Cryptography.Xml;
using System.Security;
using System.Xml.Linq;
using Trasen.PaperFree.Application.MedicalRecord.Dto.FileTable;
using Trasen.PaperFree.Application.MedicalRecord.Dto.PatientDetails;
using Trasen.PaperFree.Application.MedicalRecord.Query.PatientDetails;
using Trasen.PaperFree.Application.SystemBasicInfo.Dto.BaseWatermark;
using Trasen.PaperFree.Domain.ArchiveRecord.Repository;
using Trasen.PaperFree.Domain.IgnoreItme.Entity;
using Trasen.PaperFree.Domain.IgnoreItme.Repository;
using Trasen.PaperFree.Domain.PatientDetails.Repository;
using Trasen.PaperFree.Domain.Shared.Extend;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;
using System.Linq;
using Pipelines.Sockets.Unofficial.Arenas;

namespace Trasen.PaperFree.Application.MedicalRecord.Handlers.PatientDetails
{
    internal class FindPatientDetailsHandlers : IRequestHandler<FindPatientDetailsQry, PatientDetailsDto>
    {
        private readonly IFilesHisRepo _filesHisRepo;
        private readonly IFilesOtherRepo _filesOtherRepo;
        private readonly IBaseWatermarkRepo _baseWatermarkRepo;
        private readonly IOutpatientInfoRepo outpatientInfoRepo;
        private readonly IDeptMenuTreeRepo deptMenuTreeRepo;
        private readonly IArchiverMeumRepo _archiverMeumRepo;
        private readonly IIgnoreItmeRepo _ignoreItmeRepo;
        private List<Domain.ArchiveRecord.Entity.ArchiverMeum> archiverMeums;
         private List<IgnoreItmeTable> ignoreItmeTable;
        private List<FileTableDto> fileTables;

        public FindPatientDetailsHandlers(IOutpatientInfoRepo outpatientInfoRepo, IDeptMenuTreeRepo deptMenuTreeRepo, IArchiverMeumRepo archiverMeumRepo, IFilesHisRepo filesHisRepo, IFilesOtherRepo filesOtherRepo, IBaseWatermarkRepo baseWatermarkRepo, IIgnoreItmeRepo ignoreItmeRepo)
        {
            this.outpatientInfoRepo = outpatientInfoRepo;
            this.deptMenuTreeRepo = deptMenuTreeRepo;
            _archiverMeumRepo = archiverMeumRepo;
            _filesHisRepo = filesHisRepo;
            _filesOtherRepo = filesOtherRepo;
            _baseWatermarkRepo = baseWatermarkRepo;
            _ignoreItmeRepo = ignoreItmeRepo;
        }

        public async Task<PatientDetailsDto> Handle(FindPatientDetailsQry request, CancellationToken cancellationToken)
        {
            var watermark =  _baseWatermarkRepo.QueryAll().AsNoTracking().Where(x => x.OrgCode == request.OrgCode && x.HospCode == request.HospCode ).Select(_ => new PatientBaseWatermarkDto
            {

                WatermarkName = _.WatermarkName,
                UseScene = _.UseScene,
                Color = _.Color,
                Xstation = _.Xstation,
                Ystation = _.Ystation,
                Angle = _.Angle,
                Direction = _.Direction,
                Font = _.Font,
                FontSize=_.FontSize,
                Pellucidity = _.Pellucidity,
                Hight = _.Hight,
                Width = _.Width,
                Picture = _.Picture,
                PicX = _.PicX,
                PicY = _.PicY,
                GapX=_.GapX,
                GapY=_.GapY,
            }).ToListAsync();


            var outpatient =  outpatientInfoRepo.QueryAll().AsNoTracking().Where(x => x.ArchiveId == request.ArchiveId).Select(x => new PatientDetailsDto
            {
                Admiss_Id = x.AdmissId,
                Name = x.Name,
                OutDate = x.OutDate,
                Status = x.Status,
                StatusName = x.Status.ToDescription().ToString(),
            }).FirstOrDefaultAsync();
            var list = await deptMenuTreeRepo.QueryAll().AsNoTracking().Select(x => new DeptMenuTreeFileListDto
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
            }).WhereIf(_ => _.OrgCode == request.OrgCode, !string.IsNullOrWhiteSpace(request.OrgCode))
            .WhereIf(_ => _.HospCode == request.HospCode, !string.IsNullOrWhiteSpace(request.HospCode))
             .WhereIf(_ => _.DeptId == request.DeptId, !string.IsNullOrWhiteSpace(request.DeptId))
             .ToListAsync();
            NewPatientFile(request);
             archiverMeums = _archiverMeumRepo.QueryAll().AsNoTracking().ToList(); ;
             ignoreItmeTable = _ignoreItmeRepo.QueryAll().AsNoTracking().Where(x => x.ArchiveId == request.ArchiveId).ToList();
        List<DeptMenuTreeFileListDto> tree = BuildTree(list, null);
            var data = await outpatient;
            var water = await watermark;

            data!.WatermarkDataDto = water;
            data!.deptMenuTreeFileListDtos = tree;
            return data;
        }

        /// <summary>
        /// 合并文件数据
        /// </summary>
        /// <param name="request"></param>
        private void NewPatientFile(FindPatientDetailsQry request)
        {
            var fileHis = _filesHisRepo.QueryAll().AsNoTracking()
                .WhereIf(x => x.ArchiveId == request.ArchiveId, !string.IsNullOrWhiteSpace(request.ArchiveId))
                .Select(x => new FileTableDto
                {
                    MeumId = x.MeumId,
                    FileId = x.FileId,
                    FileSavename = x.FileSavename,
                    FilePath = x.FilePath,
                    OrderNo = x.OrderNo
                }).ToList();
            var fileOther = _filesOtherRepo.QueryAll().AsNoTracking()
                .WhereIf(x => x.ArchiveId == request.ArchiveId, !string.IsNullOrWhiteSpace(request.ArchiveId))
                .Select(x => new FileTableDto
                {
                    MeumId = x.MenuId,
                    FileId = x.FileId,
                    FileSavename = x.FileSavename,
                    FilePath = x.FilePath,
                    OrderNo = x.OrderNo
                }).ToList(); ;
            //HIS文件数据和其他文件数据合并
            fileTables = fileHis.Concat(fileOther).ToList();
        }

        public List<DeptMenuTreeFileListDto> BuildTree(List<DeptMenuTreeFileListDto> factoryModels, string? parentid)
        {
            var treeNodes = new List<DeptMenuTreeFileListDto>();
            var joinedData = archiverMeums.Join
            (
            factoryModels,
            t1 => t1.Id,
            t2 => t2.ArchiverMeumId,
            (t1, t2) => new DeptMenuTreeFileListDto
            {
                Id= t2.Id,
                ParentId = t1.ParentId,
                IsRequired = t2.IsRequired,
                ArchiverMeumId = t2.ArchiverMeumId,
                OrgCode = t1.OrgCode,
                MenuName = t1.MenuName,
                HospCode = t2.HospCode,
                InputCode = t2.InputCode,
            }).ToList();
            foreach (var model in joinedData.Where(x => x.ParentId == parentid))
            {
                var treeNode = new DeptMenuTreeFileListDto
                {
                    Id= model.Id,
                    ParentId = model.ParentId,
                    IsRequired = model.IsRequired,
                    ArchiverMeumId = model.ArchiverMeumId,
                    OrgCode = model.OrgCode,
                    MenuName = model.MenuName,
                    HospCode = model.HospCode,
                    InputCode = model.InputCode,
                    FileTable = fileTables.Where(x => x.MeumId == model.ArchiverMeumId).ToList(),
                    IsIgnoreltme = ignoreItmeTable.Any(child => child.MeumTreeId == model.Id),
                    IgnoreltmeId = ignoreItmeTable.FirstOrDefault(child => child.MeumTreeId == model.Id)?.Id??string.Empty,
                    Children = BuildTree(joinedData, model.ArchiverMeumId),
                  
                };
                treeNodes.Add(treeNode);
            }

            return treeNodes;
        }
    }
}