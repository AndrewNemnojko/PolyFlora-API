
namespace PolyFlora.Application.DTOs.Flower
{
    public record CultureDetail
    {
        public string LangCode { get; set; } = null!;
        public bool TargCulture { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
