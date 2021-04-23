using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using PriceTickerShared;

namespace PriceTicker
{
    public class PriceTickerScreenViewModel
    {
        public ObservableCollection<AssetViewModel> AssetViewModels { get; set; }
            = new ObservableCollection<AssetViewModel>();

        private readonly Subject<Asset> _assetStream = new Subject<Asset>();

        public PriceTickerScreenViewModel()
        {
            // get UI context so can ensure updates are on correct threada
            var uiContext = SynchronizationContext.Current;

            // add the list of Assets to display
            AssetViewModels.Add(new AssetViewModel { Name = "Stock 1", Price = 0 });
            AssetViewModels.Add(new AssetViewModel { Name = "Stock 2", Price = 0 });

            // set up subscriber to asset stream subject
            _assetStream.Subscribe(asset =>
            {
                var avm = AssetViewModels.Single(a => a.Name == asset.Name);

                // update on UI context
                uiContext.Send(_ =>
                {
                    avm.Price = asset.Price;
                    avm.History.Add(new HistoryItem
                    {
                        Timestamp = asset.Timestamp,
                        Price = asset.Price
                    });
                }, null);

            });

            // connect to the price server, receive updates and push to the subject stream
            Task.Run(() =>
            {
                using (var client = new SubscriberSocket())
                {
                    client.Options.ReceiveHighWatermark = 1000;
                    client.Connect("tcp://localhost:5555");
                    client.Subscribe("Stock 1");
                    client.Subscribe("Stock 2");

                    while (true)
                    {
                        var topicMessage = client.ReceiveFrameString();
                        var message = client.ReceiveFrameString();

                        // push onto subject
                        _assetStream.OnNext(message.Deserialise<Asset>());
                    }
                }
            });
        }
    }
}