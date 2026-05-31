using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Entities
{
    // user <-> role <-> product permission mapping
    public class UserProductPermission
    {
        // link between user to products
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int ProductId { get; private set; }
        public User User { get; private set; }
        public Product Product { get; private set; }

        public UserProductPermission(int userId, int productId)
        {
            UserId = userId;
            ProductId = productId;
        }

        private UserProductPermission() { }
    }
}
