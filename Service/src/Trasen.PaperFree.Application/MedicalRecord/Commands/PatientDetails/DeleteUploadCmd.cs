namespace Trasen.PaperFree.Application.MedicalRecord.Commands.PatientDetails
{
    public record DeleteUploadCmd(string id) : IRequest<bool>;
}