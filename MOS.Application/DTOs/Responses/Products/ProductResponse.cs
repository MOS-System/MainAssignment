namespace MOS.Application.DTOs.Responses.Products
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public bool IsFavorite { get; set; }    // true if current user favorited it
    }
}
