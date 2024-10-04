namespace PolyFlora.Application.DTOs.Flower
{
    public class FlowerSummary
    {
        public Guid Id { get; set; } = Guid.Empty!;
        public string Name { get; set; } = null!;
        public string TName { get; set; } = null!;
        public decimal Price { get; set; } 
        public string? PicturePath { get; set; }
    }
}
