namespace CRM.BLL.DTOs.Responese
{
    public class FailedFileUploadDto
    {
        public string FileName { get; set; } = null!;
        public string Reason { get; set; } = null!;
    }
}