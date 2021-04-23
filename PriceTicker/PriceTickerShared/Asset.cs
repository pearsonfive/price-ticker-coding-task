using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceTickerShared
{
    public class Asset
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
