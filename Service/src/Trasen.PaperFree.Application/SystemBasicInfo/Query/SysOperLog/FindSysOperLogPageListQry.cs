using Trasen.PaperFree.Application.SystemBasicInfo.Dto.SysOperLog;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.SysOperLog
{
    /// <summary>
    ///
    /// </summary>
    public record FindSysOperLogPageListQry : IRequest<PageData<List<SysOperLogPageDto>>>
    {
        /// <summary>
        /// 操作模块
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public BusinessType? BusinessType { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        public string? RequestType { get; set; }
        /// <summary>
        /// 操作类别
        /// </summary>
        public BperatorType? OperatorType { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string? OperName { get; set; }
        /// <summary>
        /// 操作状态
        /// </summary>
        public StatusLogType? Status { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string? OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string? HospCode { get; set; }
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
        public FindSysOperLogPageListQry SetPagePram(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            return this;
        }
    }
}