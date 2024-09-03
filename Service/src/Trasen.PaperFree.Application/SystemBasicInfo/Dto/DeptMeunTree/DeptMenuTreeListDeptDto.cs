using Trasen.PaperFree.Domain.FileTable.Entity;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.DeptMeunTree
{
    public record DeptMenuTreeListDeptDto
    {
        public string Id { get; set; }
        /// <summary>
        /// 科室编码
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
        /// 排序ID
        /// </summary>
        public int OrderbyId {  get; set; }
        public List<DeptMenuTreeListDeptDto> Children { get; set; } = new List<DeptMenuTreeListDeptDto>();

        public List<FilesHis> his { get; set; } = new List<FilesHis>();
    }
}