
using Microsoft.AspNetCore.Http;

namespace PolyFlora.Application.DTOs.Flower
{
    public record FlowerRequest
    (
       ICollection<CultureDetail> CultureDetails,
       decimal Price,
       int InStock,
       IFormFile? ImageFile,
       Guid? ParentId,
       ICollection<Guid>? ChildrensIds
    );
}
