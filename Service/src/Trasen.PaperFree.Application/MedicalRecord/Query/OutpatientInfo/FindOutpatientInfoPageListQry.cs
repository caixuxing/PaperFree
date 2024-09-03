using Trasen.PaperFree.Application.MedicalRecord.Dto.OutpatientInfo;

namespace Trasen.PaperFree.Application.MedicalRecord.Query.OutpatientInfo
{
    /// <summary>
    /// 查询入参
    /// </summary>
    public record FindOutpatientInfoPageListQry : IRequest<PageData<List<OutpatientInfoDto>?>>
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string? AdmissId { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string? OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string? HospCode { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 出院时间
        /// </summary>
        public DateTime? BeginOutDate { get; set; }
        /// <summary>
        /// 出院时间
        /// </summary>
        public DateTime? EndOutDate { get; set; }
        /// <summary>
        /// 出院科室
        /// </summary>
        public string? OutDeptCode { get; set; }
        /// <summary>
        /// 是否逾期
        /// </summary>
        public string? IsOverdate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public WorkFlowState? Status { get; set; }
        /// <summary>
        /// 分页索引
        /// </summary>
        public int PageIndex { get; private set; }
        /// <summary>
        /// 页码大小
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 设置分页参数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public FindOutpatientInfoPageListQry SetPageParm(int pageIndex, int pagesize)
        {
            PageIndex = pageIndex;
            PageSize = pagesize;
            return this;
        }
    }
}