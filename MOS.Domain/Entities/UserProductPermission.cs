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
        public Guid Id { get; private set; }
        public PermissionLevel PermissionLevel { get; private set; }
        public DateTime AssignedAt { get; private set; } = DateTime.UtcNow;

        //Relations
        public Guid UserId { get; private set; }
        public User? User { get; private set; }
        public Guid ProductId { get; private set; }
        public Product? Product { get; private set; }

        public UserProductPermission(Guid userId, Guid productId, DateTime assignedAt, PermissionLevel p
            )
        {
            UserId = userId;
            ProductId = productId;
            AssignedAt = assignedAt;
            PermissionLevel = p;
        }

        private UserProductPermission() { }
    }
}
