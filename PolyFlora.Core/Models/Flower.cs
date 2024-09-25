
namespace PolyFlora.Core.Models
{
    public class Flower
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TName { get; set; } = string.Empty; //TransliteratedName
        public string PictureName = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public Flower? FlowerParent { get; set; }
        public ICollection<Flower> FlowerChildrens { get; set; }
            = new List<Flower>();
        public ICollection<Bouquet> Bouquets { get; set; }
            = new List<Bouquet>();
    }
}
