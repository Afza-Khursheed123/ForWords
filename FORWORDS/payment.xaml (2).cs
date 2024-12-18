using BookShop;
using System;
using System.Collections;
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
using System.Windows.Shapes;

namespace FORWORDS
{
    public partial class payment : Window
    {

        public payment()
        {
            InitializeComponent();
        }

        // Private fields for storing input data
        string customerName;
        string customerNo;
        string customerAddress;
        string customerCity;
        string customerCountry;
        string customerEmail;

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerName = name.Text;
        }

        private void contact_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerNo = contact.Text;
        }

        private void address_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerAddress = address.Text;
        }

        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerEmail = email.Text;
        }

        private void city_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerCity = city.Text;
        }

        private void country_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerCountry = country.Text;
        }

        // Method to gather customer data
        public CustomerDataModel GetCustomerData()
        {
            return new CustomerDataModel
            {
                Name = customerName,
                ContactNo = customerNo,
                Address = customerAddress,
                Email = customerEmail,
                City = customerCity,
                Country = customerCountry
            };
        }

        // Button click handler to navigate to the receipt page
        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            var customerData = GetCustomerData();
            var receiptPage = new receipt(customerData);
            receiptPage.Show();
            this.Close(); // Optional: Close the current window
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var customerData = GetCustomerData();

            var receiptPage = new receipt(customerData);

            receiptPage.Show();

            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            CartWindow mainWindow = new CartWindow();
            mainWindow.Show();
        
    }
        private void Home(object sender, RoutedEventArgs e)
        {
            this.Hide();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();


        }



      

        private void About(object sender, RoutedEventArgs e)
        {
            this.Hide();


            About aboutWindow = new About();
            aboutWindow.Show();



            this.Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var cartWindow = new CartWindow();
            cartWindow.Show();
            this.Close();
        }
    }
}

