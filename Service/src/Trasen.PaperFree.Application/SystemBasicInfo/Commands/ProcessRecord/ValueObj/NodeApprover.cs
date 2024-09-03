namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord.ValueObj;

/// <summary>
/// 节点审批人信息
/// </summary>
public class NodeApprover
{
    /// <summary>
    /// 审批ID
    /// </summary>
    public string ApproverId { get; set; } = string.Empty;

    /// <summary>
    /// 审批人账户
    /// </summary>
    public string ApproverAccount { get; set; } = string.Empty;

    /// <summary>
    /// 审批人姓名
    /// </summary>
    public string ApproverName { get; set; } = string.Empty;
}

/// <summary>
/// 审批人员校验规则
/// </summary>
public class NodeApproverValidator : AbstractValidator<NodeApprover>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public NodeApproverValidator()
    {
        RuleFor(item => item.ApproverId).NotEmpty().WithMessage("审批人ID不能为空！")
             .MaximumLength(50).WithMessage("审批人ID长度不能超过50个字符！");
        RuleFor(item => item.ApproverAccount).NotEmpty().WithMessage("审批人账户不能为空！")
            .MaximumLength(30).WithMessage("审批人账户长度不能超过30个字符！");
        RuleFor(item => item.ApproverName).NotEmpty().WithMessage("审批人姓名不能为空！")
          .MaximumLength(30).WithMessage("审批人姓名长度不能超过30个字符！");
    }
}