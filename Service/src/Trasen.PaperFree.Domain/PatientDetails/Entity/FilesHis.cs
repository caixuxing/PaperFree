using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.FileTable.Entity
{
    public record FilesHis : FullRoot
    {
        private FilesHis() { }
        public FilesHis changFilesHis(string filesavename) {
            this.FileSavename=filesavename;
            return this;
        }
        //病历目录表ID 病历目录表ID
        //档案号ID 档案号ID
        public string ArchiveId { get; init; }
        //病历目录表ID
        public string MeumId { get; init; }
        //文件唯-ID
        public string FileId { get; init; }
        //文件原名称
        public string? FileTruename { get; private set; }
        //文件保存后名称
        public string? FileSavename { get; private set; }
        //文件类型格式 文件类型格式
        public string? FileType { get; private set; }
        //文件存储路径
        public string FilePath { get; private set; }
        //文件大小 文件大小
        public string? FileSize { get; private set; }
        //排序号
        public string? OrderNo { get; private set; }
        //状态【物理文件上传状态】 状态【物理文件上传状态】
        public string Status { get; private set; }
        //文件存储状态【临时、正式】 文件存储状态【临时、正式】
        public string? StorageState { get; private set; }
        /// <summary>
        /// 数据类型  1 门诊 2 住院
        /// </summary>
        public string? DataType { get; private set; }
        //备注 备注
        public string? Remark { get; private set; }
    }
}