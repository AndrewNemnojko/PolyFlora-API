
namespace PolyFlora.Core.Models
{
    public class Flower
    {
        public Guid Id { get; set; }        
        public string TName { get; set; } = string.Empty; //TransliteratedName
        public ICollection<FlowerCulture> CultureDetails { get; set; }
            = new List<FlowerCulture>();
        public Image? Image {  get; set; }      
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
