using Trasen.PaperFree.Application.MedicalRecord.Dto.PatientDetails;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.MedicalRecord.Dto.FileTable
{
    public record DeptMenuTreeFileListDto
    {
        public string Id {  get; set; }
        /// <summary>
        /// 出院科室
        /// </summary>
        public string DeptId { get; set; }
        /// <summary>
        /// 归档目录ID
        /// </summary>
        public string ArchiverMeumId { get; set; }
        /// <summary>
        /// 父目录ID
        /// </summary>
        public string? ParentId { get; set; }
        /// <summary>
        /// 目录名称
        /// </summary>
        public string? MenuName { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public WhetherType IsRequired { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }
        /// <summary>
        /// 所属辖区
        /// </summary>
        public string InputCode { get; set; }
        /// <summary>
        /// 是否忽略
        /// </summary>
        public bool IsIgnoreltme {  get; set; }
        /// <summary>
        /// 忽略文件数据ID
        /// </summary>
        public string IgnoreltmeId {  get; set; }
        public List<DeptMenuTreeFileListDto> Children { get; set; } = new List<DeptMenuTreeFileListDto>();

        public List<FileTableDto> FileTable { get; set; } = new List<FileTableDto>();
    }
}