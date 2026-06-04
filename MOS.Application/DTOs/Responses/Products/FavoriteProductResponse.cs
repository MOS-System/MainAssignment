
namespace MOS.Application.DTOs.Responses.Products
{
    public class FavoriteProductResponse
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string IconUrl { get; set; } = string.Empty;

        public DateTime AddedAt { get; set; }
    }
}
