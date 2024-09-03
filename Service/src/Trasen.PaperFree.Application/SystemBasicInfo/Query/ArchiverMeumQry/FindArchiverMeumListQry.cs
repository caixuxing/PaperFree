using Trasen.PaperFree.Application.SystemBasicInfo.Dto.ArchiverMeumDto;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.ArchiverMeumQry
{
    /// <summary>
    /// 入参
    /// </summary>
    public record FindArchiverMeumListQry : IRequest<List<ArchiverMeumListDto>?>
    {
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }
    }
}