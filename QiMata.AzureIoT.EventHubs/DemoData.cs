using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiMata.AzureIoT.EventHubs
{
    class DemoData
    {
        public DemoData()
        {
            RandomGuid = Guid.NewGuid();
        }

        public int DeviceId { get; set; }

        public Guid RandomGuid { get; set; }

        public int Temperature { get; set; }
    }
}
