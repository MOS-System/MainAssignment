using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Entities
{
    // user's favorite products
    public class FavoriteService
    {
        public Guid Id { get; private set; }
        public DateTime AddedAt { get; private set; }
        

        //Relations
        public Guid UserId { get; private set; }
        public User? User { get; private set; }
        public Guid ProductId { get; private set; }
        public Product? Product { get; private set; }
        public FavoriteService(Guid userId, Guid productId)
        {
            UserId = userId;
            ProductId = productId;
            AddedAt = DateTime.UtcNow;
        }

        private FavoriteService() { }
    }
}
