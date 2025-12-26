namespace CRM.BLL.DTOs.Responese
{
    public class FileValidationResponseDto
    {
        public bool IsValid { get; set; }
        public string FileName { get; set; } = null!;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public string ValidationMessage { get; set; } = null!;
    }
}