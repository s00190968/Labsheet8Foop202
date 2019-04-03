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

namespace Exercise1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NORTHWNDEntities db = new NORTHWNDEntities();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnEx1_Click_1(object sender, RoutedEventArgs e)
        {
            var q = db.Customers.
                GroupBy(c => c.Country).//c.country turns to key
                OrderBy(c => c.Count()).
                Select(c => new
                {
                    Country = c.Key,//this key here
                    Count = c.Count()
                });

            dgrCustomersQ1.ItemsSource = q.ToList();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            var q = db.Customers.
                Where(c => c.Country == "Italy").
                OrderBy(c => c.CompanyName).
                Select(c => c);

            dgrCustomersQ2.ItemsSource = q.ToList();
        }
    }
}
