
namespace PolyFlora.Core.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
        public long FileSize { get; set; } 
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}
