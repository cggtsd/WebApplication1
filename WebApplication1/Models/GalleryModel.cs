using WebApplication1.Data;

namespace WebApplication1.Models
{
    public class GalleryModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public Books Book { get; set; }
    }
}
