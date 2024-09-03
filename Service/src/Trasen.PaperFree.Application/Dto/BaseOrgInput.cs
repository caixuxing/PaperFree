using System.ComponentModel.DataAnnotations;

namespace Trasen.PaperFree.Application.Dto
{
    public record BaseOrgInput
    {
        /// <summary>
        /// 机构编码
        /// </summary>
        [Required]
        public string OrgCode { get; set; }

        /// <summary>
        /// 院区编码
        /// </summary>
        [Required]
        public string HospCode { get; set; }
    }
}