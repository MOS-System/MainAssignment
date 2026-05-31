using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Entities
{
    // user's favorite products
    public class FavoriteService
    {
        public int Id { get; private set; }
        public DateTime AddedAt { get; private set; }
        

        //Relations
        public int UserId { get; private set; }
        public User? User { get; private set; }
        public int ProductId { get; private set; }
        public Product? Product { get; private set; }
        public FavoriteService(int userId, int productId)
        {
            UserId = userId;
            ProductId = productId;
            AddedAt = DateTime.UtcNow;
        }

        private FavoriteService() { }
    }
}
