﻿using System;
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

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            var q = db.Products.
                Where(p => p.UnitsInStock - p.UnitsOnOrder > 0).
                OrderBy(p => p.ProductName).
                Select(p => new
                {
                    Product = p.ProductName,
                    Available = p.UnitsInStock - p.UnitsOnOrder
                });

            dgrCustomersQ3.ItemsSource = q.ToList();
        }
        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            var q = db.Order_Details.
                Where(p => p.Discount > 0).
                OrderBy(p => p.Product.ProductName).
                Select(p => new
                {
                    ProductName = p.Product.ProductName,
                    DiscountGiven = p.Discount,
                    OrderID = p.OrderID
                });

            dgrCustomersQ4.ItemsSource = q.ToList();
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            var q = db.Orders.Select(o => o.Freight);
            totalTxBl.Text = string.Format($"€ {q.Sum()}");
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            var q = db.Products.
                OrderBy(p => p.Category.CategoryName).
                ThenBy(p => p.ProductName).
                Select(p => new
                {
                    p.CategoryID,
                    p.Category.CategoryName,
                    p.ProductName,
                    p.UnitPrice
                });

            dgrCustomersQ6.ItemsSource = q.ToList();
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            var q = db.Orders.
                GroupBy(p => p.CustomerID).
                OrderByDescending(c => c.Count()).
                Select(p => new
                {
                    CustomerId = p.Key,
                    NumberOfOrders = p.Count()
                });

            dgrCustomersQ7.ItemsSource = q.ToList().Take(10);
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            var q = from o in db.Orders
                    group o by o.CustomerID into g
                    join c in db.Customers on g.Key equals c.CustomerID
                    orderby g.Count() descending
                    select new
                    {
                        CustomerID = c.CustomerID,
                        CompanyName = c.CompanyName,
                        NumberOfOrders = c.Orders.Count
                    };
            dgrCustomersQ8.ItemsSource = q.ToList().Take(10);
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            var q = db.Customers.
                Where(c => c.Orders.Count <1).
                OrderBy(c => c.Orders.Count).
                Select(c => new
                {
                    CustomerID = c.CustomerID,
                    CompanyName = c.CompanyName,
                    NumberOfOrders = c.Orders.Count
                });

            dgrCustomersQ9.ItemsSource = q.ToList();
        }
    }
}
