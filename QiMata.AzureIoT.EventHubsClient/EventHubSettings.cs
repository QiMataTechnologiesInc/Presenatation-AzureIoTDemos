using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiMata.AzureIoT.EventHubsClient
{
    public class EventHubSettings
    {
        public string ServiceBusUrl { get; set; }

        public string SharedAccessSignature { get; set; }

        public string ServiceBusNamespace { get; set; }

        public string HubName { get; set; }

    }
}
