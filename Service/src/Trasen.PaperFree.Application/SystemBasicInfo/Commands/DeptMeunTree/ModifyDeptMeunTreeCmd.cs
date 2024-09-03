using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;
using Trasen.PaperFree.Domain.SystemBasicData.Repository;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.DeptMeunTree
{
    /// <summary>
    /// 必传文件批量构建修改指令
    /// </summary>
    /// <param name="cmd"></param>
    public record ModifyDeptMeunTreeCmdsListCmd(ModifyDeptMeunTreeCmd cmd) : IRequest<bool>;
    public record ModifyDeptMeunTreeCmd
    {
        /// <summary>
        /// 科室编码
        /// </summary>
        [Required]
        public string[] DeptId { get; set; }
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
        /// <summary>
        /// 所属辖区
        /// </summary>
        [Required]
        public string InputCode { get; set; }
        [Required]
        public List<TreeDatalist> ListTreeData { get; set; }
        public class ModifyDeptMeunTreeCmdValidate : AbstractValidator<ModifyDeptMeunTreeCmd>
        {
            public ModifyDeptMeunTreeCmdValidate()
            {
                //RuleFor(x => x.DeptId).NotEmpty().WithMessage("科室编码不能为空！").MaximumLength(100).WithMessage("科室编码长度不能超过100个字符");
                RuleFor(x => x.InputCode).NotEmpty().WithMessage("辖区编码不能为空！").MaximumLength(20).WithMessage("辖区编码长度不能超过20个字符");
                RuleFor(x => x.OrgCode).NotEmpty().WithMessage("机构编码不能为空！").MaximumLength(20).WithMessage("机构编码长度不能超过20个字符");
                RuleFor(x => x.HospCode).NotEmpty().WithMessage("院区编码不能为空！").MaximumLength(20).WithMessage("院区编码长度不能超过20个字符");
            }
        }
    }
    public record TreeDatalist
    {
        /// <summary>
        /// 归档目录ID
        /// </summary>
        [Required]
        public string ArchiverMeumId { get; set; }
        /// <summary>
        /// 父目录ID
        /// </summary>
        public string? ParentId { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        [Required]
        public WhetherType IsRequired { get; set; }

        public class ModifyTreeDatelistValidate : AbstractValidator<TreeDatalist>
        {
            private IDeptMenuTreeRepo deptMenuTreeRepo;

            public ModifyTreeDatelistValidate(IDeptMenuTreeRepo menuTreeRepo )
            {
                this.deptMenuTreeRepo = menuTreeRepo;
                RuleFor(x => x.ArchiverMeumId).NotEmpty().WithMessage("目录编码不能为空！").MaximumLength(50).WithMessage("目录编码长度不能超过50个字符");
                RuleFor(x => x.ParentId).MaximumLength(50).WithMessage("父目录ID长度不能超过50个字符");
            }
            //public bool IsTreeDatelist(TreeDatalist treeDatalist,string archiverid) {
            //  //  !deptMenuTreeRepo.QueryAll().AsNoTracking().Any()
            //    return true;
            //}
        }
    }
}