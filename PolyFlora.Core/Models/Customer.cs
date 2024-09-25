
namespace PolyFlora.Core.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public User? User { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
