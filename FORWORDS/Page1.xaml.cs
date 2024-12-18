using FORWORDS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookShop
{
    public partial class CartWindow : Window
    {
        public ObservableCollection<CartItem> CartItems { get; set; }

        public CartWindow()
        {
            InitializeComponent();

            CartItems = new ObservableCollection<CartItem>();
            CartItemsList.ItemsSource = CartItems;

            LoadCartItems();

            UpdateTotalPrice();
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

        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button.DataContext as CartItem;
            if (item != null)
            {
                CartItems.Remove(item);

                UpdateTotalPrice();

                RemoveItemFromDatabase(item.Id);
            }
        }

        private void RemoveItemFromDatabase(int id)
        {
            try
            {
                string connectionString = "Server=localhost;Database=s_cart;Uid=root;Pwd=Afza23001!!!;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM sCart_details WHERE Id = @Id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing item from database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProceedToCheckout_Click(object sender, RoutedEventArgs e)
        {

            payment paymentwindow = new payment();
            paymentwindow.Show();


            this.Close();
        }

        private void UpdateTotalPrice()
        {
            decimal total = CartItems.Sum(item => item.Total);
            TotalPriceText.Text = total.ToString("F2");
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cartWindow = new CartWindow();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void Home(object sender, RoutedEventArgs e)
        {
            this.Hide();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void About(object sender, RoutedEventArgs e)
        {
            this.Hide();

            About aboutWindow = new About();
            aboutWindow.Show();


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
