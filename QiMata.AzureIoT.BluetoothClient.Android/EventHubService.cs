using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QiMata.AzureIoT.EventHubsClient.Helpers;
using QiMata.AzureIoT.EventHubsClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace QiMata.AzureIoT.BluetoothClient.Android
{
    [Service]
    class EventHubService : Service
    {
        private static EventHubsClient.EventHubSettings EventHubSettings = new EventHubsClient.EventHubSettings
        {
            HubName = "qimatainternetofthingsdemo",
            SharedAccessSignature = "/6nQECS7ufRmPlFSUBsQK9DpY2RsRLprkygxyH0UGEw=",
            ServiceBusNamespace = "QiMataInternetOfThingsDemo-ns"
        };

        private EventHubsClient.EventHubClient _eventHubClient;
        private StatisticalContainer<EventHubData> _statisicalContainer;
        private Timer _bluetoothTimer;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            _eventHubClient = new EventHubsClient.EventHubClient(EventHubSettings);
            _statisicalContainer = new StatisticalContainer<EventHubData>(WriteValuesToEventHub);

            //Read the bluetooth values every .1 seconds and send it up to the hub every 10
            _bluetoothTimer = new Timer(delegate(object state) { Task.Factory.StartNew(async () => { await ReadBluetoothValuesAsync(); }); },
                null,0,100);
            

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            if (_eventHubClient != null)
            {
                _eventHubClient.Dispose();
            }
        }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        private async Task ReadBluetoothValuesAsync()
        {
            //TODO: implement bluetooth service
            _statisicalContainer.Add(new EventHubData());
        }

        private void WriteValuesToEventHub(IEnumerable<EventHubData> values)
        {
            var tasks = values.Select(x => _eventHubClient.SendEventAsync(x,CancellationToken.None));
            Task.WaitAll(tasks.ToArray());
        }
    }
}