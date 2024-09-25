
namespace PolyFlora.Core.Models
{
    public class Recipient
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
