using Trasen.PaperFree.Application.SystemBasicInfo.Dto.BaseBorrowMode;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.BaseBorrowMode;
public record FindBaseBorrowModeByIdQry(string Id) : IRequest<BaseBorrowModeDto>;