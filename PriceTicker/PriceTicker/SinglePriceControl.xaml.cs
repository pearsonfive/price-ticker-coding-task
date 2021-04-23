using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PriceTicker
{
    /// <summary>
    /// Interaction logic for SinglePriceControl.xaml
    /// </summary>
    public partial class SinglePriceControl : UserControl
    {
        public SinglePriceControl()
        {
            InitializeComponent();
        }

        private void Price_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var assetViewModel = (AssetViewModel) ((ContentControl) sender).DataContext;
            var historyScreen = new HistoryScreen(assetViewModel);
            historyScreen.Show();
        }
    }
}
