using Trasen.PaperFree.Application.SystemBasicInfo.Dto.EssentialDocument;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.EssentialDocument
{
    public record FindEssentialDocumentPageListQry : IRequest<PageData<List<EssentialDocumentsPageListDto>?>>
    {
        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }

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
        public FindEssentialDocumentPageListQry SetPageParm(int pageIndex, int pagesize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pagesize;
            return this;
        }
    }
}