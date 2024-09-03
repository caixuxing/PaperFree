using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord.ValueObj;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;

/// <summary>
/// 创建流程设计
/// </summary>
public record CreateWorkDesignCmd : IRequest<string>
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
}

/// <summary>
/// 创建流程设计指令校验
/// </summary>
public class CreateWorkDesignValidate : AbstractValidator<CreateWorkDesignCmd>
{
    /// <summary>
    ///验证规则
    /// </summary>
    public CreateWorkDesignValidate()
    {
        RuleFor(x => x.ProcessName).NotEmpty().WithMessage("流程名称不能为空！")
            .MaximumLength(20).WithMessage("流程名称长度不能超过20个字符")
            .Must(IsChinese).WithMessage($"必须是中文");
        RuleFor(x => x.processTempType).IsInEnum().WithMessage("无效的流程模板类型值");
        RuleFor(x => x.DeptCode).NotEmpty().WithMessage("科室编码不能为空！")
          .MaximumLength(20).WithMessage("科室编码长度不能超过20个字符");
        RuleFor(x => x.OrgCode).NotEmpty().WithMessage("机构编码不能为空！")
         .MaximumLength(20).WithMessage("机构编码长度不能超过20个字符");
        RuleFor(x => x.HospCode).NotEmpty().WithMessage("院区编码不能为空！")
         .MaximumLength(20).WithMessage("院区编码长度不能超过20个字符");
        RuleFor(x => x.workNodes).NotEmpty().WithMessage("流程节点不能为空！");
        RuleForEach(node => node.workNodes).SetValidator(new WorkNodeValidate());
    }
    /// <summary>
    /// 是否为中文
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public bool IsChinese(string val)
    {
        return new Regex(@"^[\u4e00-\u9fa5]+$").IsMatch(val);
    }
}