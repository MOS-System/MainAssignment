using MOS.Domain.Entities;
using MOS.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Entities
{
    // stores code + expiry timestamp
    public class MfaCode
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public bool IsUsed { get; private set; }


        //Relations
        public Guid UserId { get; private set; }
        public User? User { get; private set; }
        
        
        public MfaCode(Guid userId, string code)
        {
            UserId = userId;
            Code = code;
            ExpiresAt = DateTime.UtcNow.AddMinutes(MfaConstants.CodeExpiryMinutes);
            IsUsed = false;
        }

        private MfaCode() { }

        public void MarkAsUsed()
        {
            // TODO: implement
            throw new NotImplementedException();
        }

        public bool IsExpired()
        {
            // TODO: implement
            throw new NotImplementedException();
        }
    }
}
