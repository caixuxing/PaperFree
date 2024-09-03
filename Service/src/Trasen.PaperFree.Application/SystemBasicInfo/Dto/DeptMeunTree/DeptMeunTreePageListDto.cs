using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto.DeptMeunTree
{
    public record DeptMeunTreePageListDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptId { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }
        ///// <summary>
        ///// 归档目录ID
        ///// </summary>
        //public string ArchiverMeumId { get; set; }
        ///// <summary>
        ///// 父目录ID
        ///// </summary>
        //public string? ParentId { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        //public WhetherType IsRequired { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string HospName { get; set; }
        /// <summary>
        /// 所属辖区
        /// </summary>
        public string InputCode { get; set; }
        /// <summary>
        /// 所属辖区
        /// </summary>
        public string InputName { get; set; }
    }
}