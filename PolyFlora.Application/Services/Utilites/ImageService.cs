using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PolyFlora.Application.DTOs.Common;
using PolyFlora.Application.Interfaces.Utilites;
using PolyFlora.Core.Models;

namespace PolyFlora.Application.Services.Utilites
{
    public class ImageService : IImageService
    {
        private readonly string _privateMediaRootPath;
        private readonly string _publicMediaRootPath;

        public ImageService(IConfiguration configuration, IHostEnvironment env)
        {
            _privateMediaRootPath = Path.Combine(env.ContentRootPath, configuration["MediaRootPath:private"]!);
            _publicMediaRootPath = configuration["MediaRootPath:public"]!;
        }
        public async Task<ImageProcessResult> DeleteAsync(string fileName)
        {
            var result = new ImageProcessResult();

            try
            {
                var filePath = Path.Combine(_privateMediaRootPath, fileName);

                if (File.Exists(filePath))
                {
                    await Task.Run(() => File.Delete(filePath));
                    result.Success = true;
                }
                else
                {   
                    result.Success = false;
                    result.ErrorMessage = "File not found.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = $"Error occurred while deleting file: {ex.Message}";
            }

            return result;
        }

        public async Task<ImageProcessResult> UploadAsync(IFormFile file)
        {
            var result = new ImageProcessResult();

            if (file == null || file.Length == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Invalid file.";
                return result;
            }
            try
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                var fullPath = Path.Combine(_privateMediaRootPath, fileName);

                if (!Directory.Exists(_privateMediaRootPath))
                {
                    Directory.CreateDirectory(_privateMediaRootPath);
                }

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var image = new Image
                {                   
                    FileName = fileName,
                    FilePath = fullPath,
                    FileUrl = $"{_publicMediaRootPath}/{fileName}",
                    FileSize = file.Length, 
                    TimeStamp = DateTime.UtcNow
                };

                result.Image = image;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = $"Error occurred while uploading file: {ex.Message}";
            }

            return result;
        }
    }
}
