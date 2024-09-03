using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.SystemBasicData.Entity
{
    /// <summary>
    /// 本地文件
    /// </summary>
    public record LocalFilesEntity : FullRoot
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件格式
        /// </summary>
        public string FileFormat { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public string BusinessId { get; set; }
        /// <summary>
        /// 存储方式
        /// </summary>
        public string StorageType { get; set; }
        /// <summary>
        /// 存储路劲
        /// </summary>
        public string StoragePath { get; set; }

        public string FileSource { get; set; }
    }
}