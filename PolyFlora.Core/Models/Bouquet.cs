
namespace PolyFlora.Core.Models
{
    public class Bouquet
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TName { get; set; } = string.Empty; //TransliteratedName
        public string PictureName = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Available { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public ICollection<BouquetSize> Sizes { get; set; }
            = new List<BouquetSize>();
        public ICollection<Flower> Flowers { get; set; }
            = new List<Flower>();
    }
}
