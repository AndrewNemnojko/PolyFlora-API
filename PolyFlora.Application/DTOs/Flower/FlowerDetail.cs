
namespace PolyFlora.Application.DTOs.Flower
{
    public class FlowerDetail
    {
        public Guid Id { get; set; } = Guid.Empty!;
        public string Name { get; set; } = null!;
        public string TName { get; set; } = null!;
        public string? PicturePath { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public FlowerSummary? FlowerParent { get; set; }
        public ICollection<FlowerSummary> FlowerChildrens { get; set; }
            = new List<FlowerSummary>();
    }
}
