
namespace PolyFlora.Core.Models
{
    public class FlowerPack
    {
        public Guid Id { get; set; }
        public Flower Flower { get; set; } = null!;
        public uint Quantity { get; set; }
    }
}
