using Trasen.PaperFree.Application.SystemBasicInfo.Dto.ProcessRecord;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.ProcessRecord
{
    /// <summary>
    /// 流程设计分页Qry
    /// </summary>
    public class FindProcessDesignPageListQry : IRequest<PageData<List<ProcessDesignPageListDto>>>
    {
        /// <summary>
        /// 院区编码
        /// </summary>
        public string? OrgCode { get; set; }

        /// <summary>
        /// 所属院区编码
        /// </summary>
        public string? HospCode { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string? ProcessName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 设置分页参数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public FindProcessDesignPageListQry SetPagePram(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            return this;
        }
    }

    /// <summary>
    /// 验证规则
    /// </summary>
    public class FindProcessDesignPageListValidate : AbstractValidator<FindProcessDesignPageListQry>
    {
        public FindProcessDesignPageListValidate()
        {
            RuleFor(x => x.HospCode).MaximumLength(20).WithMessage("院区编码字符长度超出!");
            RuleFor(x => x.ProcessName).MaximumLength(20).WithMessage("流程名称字符长度超出!");
            RuleFor(x => x.PageIndex).GreaterThan(0).WithMessage("分页索引值必须大于0.");
            RuleFor(x => x.PageSize).InclusiveBetween(1, 500).WithMessage("页码大小必须在1到500之间");
        }
    }
}