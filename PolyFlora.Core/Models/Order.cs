using PolyFlora.Core.Enums;

namespace PolyFlora.Core.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public uint OrderNumber { get; set; }
        public Customer Customer { get; set; } = null!;
        public Recipient? Recipient { get; set; }
        public string Notes { get; set; } = string.Empty;
        public ICollection<BouquetSize> Bouquets { get; set; }
            = new List<BouquetSize>();
        public PrepaymentType PrepaymentType { get; set; }
        public PrepaymentStatus PrepaymentStatus { get; set; }
        public string Address { get; set; } = null!;
        public string DeliveryDate { get; set; } = null!;
        public bool Anonymous { get; set; }
    }
}
