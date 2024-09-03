using System.ComponentModel.DataAnnotations.Schema;
using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.FileTable.Entity
{
    [Obsolete("暂无使用，实体暂时保留")]
    [NotMapped]
    public record FilesOtherAttached : FullRoot
    {
        private FilesOtherAttached()
        {
        }
        public FilesOtherAttached(string fileid, string uploadusercode)
        {
        }
        /// <summary>
        /// 主键GUID
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// 文件信息表ID
        /// </summary>
        public string FileId { get; private set; }
        /// <summary>
        /// 上传人ID
        /// </summary>
        public string UploadUserCode { get; private set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadDate { get; private set; }
        /// <summary>
        /// 高拍方式
        /// </summary>
        public string UploadWay { get; private set; }
        /// <summary>
        /// 高拍IP
        /// </summary>
        public string ComputerIp { get; private set; }
    }
}