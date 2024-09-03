namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto
{
    /// <summary>
    /// 人员响应模型
    /// </summary>
    public record FindOrgMemberDto
    {
        public string ORG_CODE { get; set; }

        public string HOSP_CODE { get; set; }

        public string MENBER_ID { get; set; }

        public string NAME { get; set; }
    }
}