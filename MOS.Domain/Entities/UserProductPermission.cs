using MOS.Domain.Enums;
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
        public PermissionLevel PermissionLevel { get; private set; }
        public DateTime AssignedAt { get; private set; } = DateTime.UtcNow;

        //Realtions
        public int UserId { get; private set; }
        public User? User { get; private set; }
        public int ProductId { get; private set; }
        public Product? Product { get; private set; }

        public UserProductPermission(int userId, int productId, DateTime assignedAt, PermissionLevel p
            )
        {
            UserId = userId;
            ProductId = productId;
            AssignedAt = assignedAt;
            PermissionLevel = PermissionLevel;
        }

        private UserProductPermission() { }
    }
}
