using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QiMata.AzureIoT.EventHubsClient
{
    public class EventHubClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly EventHubSettings _eventHubSettings;

        public EventHubClient(EventHubSettings eventHubSettings)
        {
            _eventHubSettings = eventHubSettings;

            _httpClient = new HttpClient {BaseAddress = new Uri(GenerateBaseUrl())};
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", GenerateAuthoriztionHeader());
        }

        public async Task SendEventAsync<T>(T eventData, CancellationToken cancellationToken)
        {
            await _httpClient.PostAsJsonAsync(GeneratePathUrl(), eventData,cancellationToken);
        }

        private string GeneratePathUrl()
        {
            return _eventHubSettings.HubName;
        }

        private string GenerateAuthoriztionHeader()
        {
            return string.Format("SharedAccessSignature sr={0}", _eventHubSettings.SharedAccessSignature);
        }

        private string GenerateBaseUrl()
        {
            return string.Format("https://{0}.servicebus.windows.net/", _eventHubSettings.ServiceBusNamespace);
        }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
            }
        }
    }
}
