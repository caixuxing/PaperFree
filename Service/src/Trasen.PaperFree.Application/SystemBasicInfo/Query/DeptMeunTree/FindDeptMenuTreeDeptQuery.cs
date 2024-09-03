using Trasen.PaperFree.Application.SystemBasicInfo.Dto.DeptMeunTree;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.DeptMeunTree
{
    public record FindDeptMenuTreeDeptQuery : IRequest<List<DeptMenuTreeListDeptDto>?>

    {
        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptId { get; set; }
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
    }
}