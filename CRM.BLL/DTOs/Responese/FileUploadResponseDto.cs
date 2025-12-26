namespace CRM.BLL.DTOs.Responese
{
    public class FileUploadResponseDto
    {
        public string FilePath { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = null!;
        public DateTime UploadedAt { get; set; }
    }
}