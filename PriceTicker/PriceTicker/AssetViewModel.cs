using System;
using System.Collections.ObjectModel;

namespace PriceTicker
{
    public class AssetViewModel : Notifier
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged($"Name");
            }
        }

        private decimal _price;

        public decimal Price
        {
            get => _price;
            set
            {
                PreviousPrice = _price;
                _price = value;
                NotifyPropertyChanged($"Price");
                NotifyPropertyChanged($"ChangeDirection");
            }
        }

        public string ChangeDirection
        {
            get
            {
                if (Price - PreviousPrice > 0) return "up";
                if (Price - PreviousPrice < 0) return "down";
                return "unch";
            }
        }

        public ObservableCollection<HistoryItem> History { get; set; } = new ObservableCollection<HistoryItem>();

        private decimal PreviousPrice { get; set; }
    }

    public class HistoryItem
    {
        public DateTime Timestamp { get; set; }
        public string DateTimeString => Timestamp.ToString("dd MMM yyyy HH:mm:ss");
        public decimal Price { get; set; }
    }
}