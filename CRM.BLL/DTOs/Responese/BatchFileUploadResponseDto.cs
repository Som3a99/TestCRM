namespace CRM.BLL.DTOs.Responese
{
    public class BatchFileUploadResponseDto
    {
        public List<FileUploadResponseDto> SuccessfulUploads { get; set; } = new();
        public List<FailedFileUploadDto> FailedUploads { get; set; } = new();
        public int TotalFiles { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
    }
}