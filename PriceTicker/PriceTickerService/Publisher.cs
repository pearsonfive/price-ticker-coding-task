using System;
using System.Collections.Generic;
using NetMQ.Sockets;
using PriceTickerShared;
using System.Reactive.Linq;
using NetMQ;

namespace PriceTickerService
{
    public class Publisher
    {
        public void Start()
        {
            // the default asset list
            var assets = new List<Asset>
            {
                new Asset {Name = "Stock 1", MinPrice = 240, MaxPrice = 270},
                new Asset {Name = "Stock 2", MinPrice = 180, MaxPrice = 210}
            };

            // setup publisher socket for NetMQ
            var publisherSocket = new PublisherSocket();
            publisherSocket.Options.SendHighWatermark = 1000;
            publisherSocket.Bind("tcp://localhost:5555");

            // publish price updates
            var r = new Random();
            Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(1000))
                .Subscribe(_ =>
                {
                    assets.ForEach(a =>
                    {
                        // get a random price including cents
                        a.Price = (decimal) (r.Next((int) a.MinPrice * 100, (int) a.MaxPrice * 100 + 1) * 0.01);
                        a.Timestamp = DateTime.UtcNow;

                        // publish to topic {Asset.Name}
                        publisherSocket.SendMoreFrame(a.Name).SendFrame(a.Serialise());
                    });
                });
        }
    }
}