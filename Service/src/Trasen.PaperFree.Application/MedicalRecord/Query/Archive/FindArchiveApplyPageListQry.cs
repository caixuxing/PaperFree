using Trasen.PaperFree.Application.MedicalRecord.Dto.Archive;

namespace Trasen.PaperFree.Application.MedicalRecord.Query.Archive
{
    /// <summary>
    /// 归档申请管理列表查询参数Qry
    /// </summary>
    public record FindArchiveApplyPageListQry : IRequest<PageData<List<ArchiveApplyPageDto>?>>
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public ProcessStatusType? CurrentStatus { get; set; }
        /// <summary>
        /// 是否结束
        /// </summary>
        public bool? IsEnd { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string? OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string? HospCode { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public string? DeptId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 病案号
        /// </summary>
        public string? AdmissId { get; set; }



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
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public FindArchiveApplyPageListQry SetPageParam(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            return this;
        }
    }

    /// <summary>
    /// 验证规则
    /// </summary>
    public class FindArchiveApplyPageListValidate : AbstractValidator<FindArchiveApplyPageListQry>
    {
        public FindArchiveApplyPageListValidate()
        {
            RuleFor(x => x.CurrentStatus).IsInEnum().WithMessage("审批状态枚举值无效");
            RuleFor(x => x.PageIndex).GreaterThan(0).WithMessage("分页索引值必须大于0.");
            RuleFor(x => x.PageSize).InclusiveBetween(1, 500).WithMessage("页码大小必须在1到500之间");
        }
    }
}