
namespace MOS.Application.DTOs.Responses.Products
{
    public class FavoriteProductResponse
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string IconUrl { get; set; }

        public DateTime AddedAt { get; set; }
    }
}
