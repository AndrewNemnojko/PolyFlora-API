
namespace PolyFlora.Core.Models
{
    public class BouquetSize
    {
        public Guid Id { get; set; }
        public Bouquet Bouquet { get; set; } = null!;
        public string SizeName { get; set; } = string.Empty;
        public string TSizeName { get; set; } = string.Empty; //TransliteratedName
        public string PictureName = string.Empty;
        public string CropDescription { get; set; } = string.Empty;
        public bool Available { get; set; }
        public decimal Price { get; set; }
        public ICollection<FlowerPack> FlowerPacks { get; set; }
            = new List<FlowerPack>();
    }
}
