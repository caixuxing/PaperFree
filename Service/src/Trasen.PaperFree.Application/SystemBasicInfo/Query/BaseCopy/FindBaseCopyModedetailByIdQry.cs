using Trasen.PaperFree.Application.SystemBasicInfo.Dto.BaseCopy;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.BaseCopy;
public record FindBaseCopyModedetailByIdQry(string Id) : IRequest<BaseCopyModedetailDto>;