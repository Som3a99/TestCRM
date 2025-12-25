using CRM.BLL.Exceptions;
using CRM.BLL.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CRM.BLL.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private const long MaxImageSize = 5 * 1024 * 1024; // 5MB
        private const long MaxVideoSize = 50 * 1024 * 1024; // 50MB
        private const long MaxAudioSize = 10 * 1024 * 1024; // 10MB

        private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private static readonly string[] AllowedVideoExtensions = { ".mp4", ".webm", ".mov" };
        private static readonly string[] AllowedAudioExtensions = { ".mp3", ".wav", ".ogg" };

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folder)
        {
            if (!ValidateImage(file))
                throw new FileUploadException("Invalid image file");

            return await UploadFileAsync(file, folder);
        }

        public async Task<string> UploadVideoAsync(IFormFile file)
        {
            if (!ValidateVideo(file))
                throw new FileUploadException("Invalid video file");

            return await UploadFileAsync(file, "videos");
        }

        public async Task<string> UploadAudioAsync(IFormFile file)
        {
            if (!ValidateAudio(file))
                throw new FileUploadException("Invalid audio file");

            return await UploadFileAsync(file, "audio");
        }

        public async Task<bool> DeleteFileAsync(string filePath)
        {
            try
            {
                var fullPath = Path.Combine(_environment.WebRootPath, filePath.TrimStart('/'));

                if (File.Exists(fullPath))
                {
                    await Task.Run(() => File.Delete(fullPath));
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidateImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            if (file.Length > MaxImageSize)
                return false;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return AllowedImageExtensions.Contains(extension);
        }

        public bool ValidateVideo(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            if (file.Length > MaxVideoSize)
                return false;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return AllowedVideoExtensions.Contains(extension);
        }

        public bool ValidateAudio(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            if (file.Length > MaxAudioSize)
                return false;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return AllowedAudioExtensions.Contains(extension);
        }

        private async Task<string> UploadFileAsync(IFormFile file, string folder)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", folder);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/uploads/{folder}/{uniqueFileName}";
        }
    }
}
