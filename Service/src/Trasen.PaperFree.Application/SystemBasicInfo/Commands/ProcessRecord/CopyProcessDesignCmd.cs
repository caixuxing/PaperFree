namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord;

/// <summary>
/// 复制流程模板指令
/// </summary>
public record CopyProcessDesignCmd(string id) : IRequest<bool>;

/// <summary>
/// 验证规则
/// </summary>
public class CopyProcessDesignValidate : AbstractValidator<CopyProcessDesignCmd>
{
    /// <summary>
    ///构造函数
    /// </summary>
    public CopyProcessDesignValidate()
    {
        RuleFor(x => x.id).NotEmpty().WithMessage("模板主键ID不能为空！")
            .MaximumLength(50).WithMessage("模板ID长度不能超过50个字符");
    }
}