using Trasen.PaperFree.Application.MedicalRecord.Dto.FileTable;

namespace Trasen.PaperFree.Application.MedicalRecord.Dto.PatientDetails
{
    public record PatientDto
    {
        
        PatientDetailsDto patientDetailsDto { get; set; } = new PatientDetailsDto();
    }
}