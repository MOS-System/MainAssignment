using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Entities
{
    public class EmailWhitelistSetting
    {
        public int Id { get; private set; }
        public bool IsEnabled { get; private set; }

        public EmailWhitelistSetting()
        {
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
