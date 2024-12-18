using BookShop;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class receipt : Window
    {
        public ObservableCollection<CartItem> CartItems { get; set; }
        private CustomerDataModel _customerData;

        public receipt(CustomerDataModel customerData)
        {
            InitializeComponent();
            _customerData = customerData;
            CartItems = new ObservableCollection<CartItem>();
            CartItemsList.ItemsSource = CartItems;
            LoadCartItems();
            UpdateTotalPrice();

            // Use the data to populate UI elements
            NameLabel.Content = _customerData.Name;
            ContactLabel.Content = _customerData.ContactNo;
            AddressLabel.Content = _customerData.Address;
            EmailLabel.Content = _customerData.Email;
            CityLabel.Content = _customerData.City;
            CountryLabel.Content = _customerData.Country;
        }


        private void LoadCartItems()
        {
            try
            {
                string connectionString = "Server=localhost;Database=s_cart;Uid=root;Pwd=Afza23001!!!;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Id, Title, Price, IMAGEURL FROM sCart_details";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        MySqlDataReader reader = command.ExecuteReader();

                        CartItems.Clear();

                        while (reader.Read())
                        {
                            CartItems.Add(new CartItem
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                                Quantity = 1
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cart items: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void UpdateTotalPrice()
        {
            decimal total = CartItems.Sum(item => item.Total);
            TotalPriceText.Text = total.ToString("F2");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            payment mainWindow = new payment();
            mainWindow.Show();
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            this.Hide();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }
    }
    public class CartItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }
 





  
}

