using Trasen.PaperFree.Domain.Base;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Domain.FileTable.Entity
{
    public record FilesOther : FullRoot
    {
        private FilesOther() { }
        public FilesOther(string archiveid, string menuid, string fileid, string filetruename, string filesavename, string filetype, string filepath,
                           WhetherType status, string sourcecode)
        {
            ArchiveId = archiveid;
            MenuId = menuid;
            FileId = fileid;
            FileTruename = filetruename;
            FileSavename = filesavename;
            FileType = filetype;
            FilePath = filepath;
            Status = status;
            SourceCode = sourcecode;
        }
        public FilesOther ChangeFilesOther(string filesavename)
        {
            this.FileSavename = filesavename;
            return this;
        }
        /// <summary>
        /// 档案号ID 
        /// </summary>
        public string ArchiveId { get; init; }
        /// <summary>
        /// 病历目录表ID 
        /// </summary>
        public string MenuId { get; init; }
        /// <summary>
        /// 文件唯-ID 
        /// </summary>
        public string FileId { get; init; }
        /// <summary>
        /// 文件原名称 
        /// </summary>
        public string? FileTruename { get; private set; }
        /// <summary>
        /// 文件保存后名称 
        /// </summary>
        public string? FileSavename { get; private set; }
        /// <summary>
        /// 文件类型格式 
        /// </summary>
        public string? FileType { get; private set; }
        /// <summary>
        /// 文件存储路径 
        /// </summary>
        public string? FilePath { get; private set; }
        /// <summary>
        /// 文件大小 
        /// </summary>
        public string? FileSize { get; private set; }
        /// <summary>
        /// 排序号 
        /// </summary>
        public string? OrderNo { get; private set; }
        /// <summary>
        /// 状态【物理文件上传状态】
        /// </summary>
        public WhetherType Status { get; private set; }
        /// <summary>
        /// 文件存储状态【临时、正式】 文件存储状态【临时、正式】
        /// </summary>
        public string? StorageState { get; private set; }

        /// <summary>
        /// 备注 备注
        /// </summary>
        public string? Remark { get; private set; }
        /// <summary>
        /// 文件来源 （系统来源）1.护理文书 2. 三测单 按此类依次往后推
        /// </summary>
        public string? SourceCode { get; private set; }
        /// <summary>
        /// 文件分类编码/ 比如首页 文件等
        /// </summary>

        public string? FileCategoryCode { get; private set; }
    }
}