using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Entities
{
    public class EmailWhitelistSetting
    {
        public int Id { get; private set; }
        public bool IsEnabled { get; private set; }

        //Relations
        public int UserId { get; private set; }
        public User? User { get; private set; }
      
        
        public EmailWhitelistSetting(int userId)
        {
            UserId = userId;
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
