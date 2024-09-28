

using PolyFlora.Core.Models;

namespace PolyFlora.Application.DTOs.Common
{
    public class ImageProcessResult
    {
        public Image? Image { get; set; }
        public bool Success { get; set; } 
        public string? ErrorMessage { get; set; } 
    }
}
