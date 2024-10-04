
using PolyFlora.Core.Enums;

namespace PolyFlora.Core.Models
{
    public class FlowerCulture
    {
        public Guid Id { get; set; }
        public LanguageCode LanguageCode { get; set; } = LanguageCode.EN;
        public bool TargCulture { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
