using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Entities
{
    public class EmailWhitelistSetting
    {
        public int Id { get; private set; }
        public bool IsEnabled { get; private set; }

        //Realtions
        public int TenantId { get; private set; }
        public Tenant? Tenant { get; private set; }
      
        
        public EmailWhitelistSetting(int tenantId)
        {
            TenantId = tenantId;
            IsEnabled = false; // off by default
        }

        public void Enable()
        {
            // TODO: implement
            throw new NotImplementedException();
        }

        public void Disable()
        {
            // TODO: implement
            throw new NotImplementedException();
        }
    }
}
