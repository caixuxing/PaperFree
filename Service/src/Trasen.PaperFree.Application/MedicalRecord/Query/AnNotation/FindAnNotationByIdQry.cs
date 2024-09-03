using Trasen.PaperFree.Application.MedicalRecord.Dto.AnNotation;

namespace Trasen.PaperFree.Application.MedicalRecord.Query.AnNotation
{
    public record FindAnNotationByIdQry(string Id) : IRequest<AnNotationDto>;
}