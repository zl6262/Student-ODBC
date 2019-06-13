using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// copyright.xaml 的交互逻辑
    /// </summary>
    public partial class copyright : Window
    {
        public copyright()
        {
            InitializeComponent();
        }

        private void Hlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;

            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }
    }
}
