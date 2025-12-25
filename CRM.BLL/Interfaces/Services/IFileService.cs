using Microsoft.AspNetCore.Http;
namespace CRM.BLL.Interfaces.Services
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(IFormFile file, string folder);
        Task<string> UploadVideoAsync(IFormFile file);
        Task<string> UploadAudioAsync(IFormFile file);
        Task<bool> DeleteFileAsync(string filePath);
        bool ValidateImage(IFormFile file);
        bool ValidateVideo(IFormFile file);
        bool ValidateAudio(IFormFile file);
    }
}
