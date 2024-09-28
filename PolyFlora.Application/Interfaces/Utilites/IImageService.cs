
using Microsoft.AspNetCore.Http;
using PolyFlora.Application.DTOs.Common;

namespace PolyFlora.Application.Interfaces.Utilites
{
    public interface IImageService
    {
        Task<ImageProcessResult> UploadAsync(IFormFile file);
        Task<ImageProcessResult> DeleteAsync(string key);
    }
}
