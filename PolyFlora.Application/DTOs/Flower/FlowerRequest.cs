
using Microsoft.AspNetCore.Http;

namespace PolyFlora.Application.DTOs.Flower
{
    public record FlowerRequest
    (
        string Name,
        string? Description,
        decimal Price,
        int InStock,
        IFormFile? ImageFile,
        Guid? ParentId,
        IList<Guid>? ChildrensIds
    );
}
