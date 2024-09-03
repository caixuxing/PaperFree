using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord.ValueObj;
using Trasen.PaperFree.Domain.ProcessRecord.Repository;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord
{
    /// <summary>
    /// 更新流程设计
    /// </summary>
    public record ModifyWorkDesignCmd : IRequest<bool>
    {
        /// <summary>
        /// 流程名称
        /// </summary>
        [Required]
        public string ProcessName { get; set; } = string.Empty;
        /// <summary>
        /// 流程模板类型
        /// </summary>
        [Required]
        public ProcessTempType processTempType { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        [Required]
        public string DeptCode { get; set; } = string.Empty;
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        [Required]
        public string OrgCode { get; set; } = string.Empty;

        /// <summary>
        /// 院区编码
        /// </summary>
        [Required]
        public string HospCode { get; set; } = string.Empty;

        /// <summary>
        /// 流程节点集合
        /// </summary>
        [Required]
        public List<WorkNode> workNodes { get; set; } = new();

        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; private set; } = string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ModifyWorkDesignCmd SetId(string id)
        {
            Id = id;
            return this;
        }
    }

    /// <summary>
    /// 验证规则
    /// </summary>
    public class ModifyWorkDesignValidate : AbstractValidator<ModifyWorkDesignCmd>
    {
        private readonly IProcessDesignRepo processDesignRepo;
        private readonly ICurrentUser currentUser;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ModifyWorkDesignValidate(IProcessDesignRepo processDesignRepo, ICurrentUser currentUser)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("更新主键ID不能为空！");
            RuleFor(x => x.ProcessName).NotEmpty().WithMessage("流程名称不能为空！")
             .MaximumLength(20).WithMessage("流程名称长度不能超过20个字符");
            RuleFor(x => x.processTempType).IsInEnum().WithMessage("无效的流程模板类型值");
            RuleFor(x => x.DeptCode).NotEmpty().WithMessage("科室编码不能为空！")
              .MaximumLength(20).WithMessage("科室编码长度不能超过20个字符");
            RuleFor(x => x.OrgCode).NotEmpty().WithMessage("机构编码不能为空！")
             .MaximumLength(20).WithMessage("机构编码长度不能超过20个字符");
            RuleFor(x => x.HospCode).NotEmpty().WithMessage("院区编码不能为空！")
             .MaximumLength(20).WithMessage("院区编码长度不能超过20个字符");
            RuleFor(x => x.workNodes).NotEmpty().WithMessage("流程节点不能为空！");
            RuleForEach(node => node.workNodes).SetValidator(new WorkNodeValidate());
            this.processDesignRepo = processDesignRepo;
            this.currentUser = currentUser;
        }

        /// <summary>
        /// 判断流程是否存在
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool IsProcessExist(ModifyProcessDesignCmd cmd, string code)
        {
            return !processDesignRepo.QueryAll().AsNoTracking().Any(_ =>
            _.ProcessCode == code &&
            _.OrgCode == cmd.OrgCode &&
            _.HospCode == cmd.HospCode &&
            _.Id != cmd.Id);
        }

        /// <summary>
        /// 同一种类型【机构、院区、流程类型】模板不能同时开启
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool IsProcess(ModifyProcessDesignCmd cmd, string code)
        {
            if (!cmd.IsEnable) return true;
            var falg = (processDesignRepo.QueryAll().AsNoTracking().Count(_ =>
            _.ProcessTempType == cmd.processTempType &&
            _.OrgCode == cmd.OrgCode &&
            _.HospCode == cmd.HospCode &&
            _.DeptCode == cmd.DeptCode &&
            _.IsEnable == true &&
            _.Id != cmd.Id) >= 1);
            return !falg;
        }
    }
}