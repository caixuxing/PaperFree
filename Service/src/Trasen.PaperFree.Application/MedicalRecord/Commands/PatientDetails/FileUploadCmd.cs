using System.ComponentModel.DataAnnotations;

namespace Trasen.PaperFree.Application.MedicalRecord.Commands.PatientDetails
{
    public record FileUploadCmd : IRequest<bool>
    {
        //档案号ID 档案号ID
        [Required]
        public string ArchiveId { get; set; }
        //病历目录表ID
        [Required]
        public string MeumId { get; set; }
        //文件保存后名称
        [Required]
        public string FileSavename { get; set; }
        //排序号
        public string? OrderNo { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
         [Required]
        public string OrgCode { get; set; }
        /// <summary>
        /// 文件流
        /// </summary>
         [Required]
        public string Base64 { get; set; }
    }
}