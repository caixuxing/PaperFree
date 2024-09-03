using Trasen.PaperFree.Application.SystemBasicInfo.Dto.EssentialDocument;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Query.EssentialDocument;
public record FindEssentialDocumentByIdQry(string Id) : IRequest<EssentialDocumentsDto>;