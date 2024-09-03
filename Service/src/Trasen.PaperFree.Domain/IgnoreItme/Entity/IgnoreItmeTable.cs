using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasen.PaperFree.Domain.Base;

namespace Trasen.PaperFree.Domain.IgnoreItme.Entity
{
    public record IgnoreItmeTable : FullRoot
    {
        private IgnoreItmeTable() { }
        public IgnoreItmeTable(string id,string archiveid,string meumtreeid) { 
        
            Id = id;
            ArchiveId = archiveid;
            MeumTreeId = meumtreeid;    
        }
        /// <summary>
        /// 档案号
        /// </summary>
        public string ArchiveId { get; set; }
        /// <summary>
        /// 必传文件主键ID
        /// </summary>
        public string MeumTreeId { get; set; }

    }
}
