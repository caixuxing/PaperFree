namespace Trasen.PaperFree.Application.MedicalRecord.Commands.AnNotation
{
    public record DeleteAnNotationTableCmd(string id) : IRequest<bool>;
}