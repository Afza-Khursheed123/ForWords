using FORWORDS;
using System;
using System.Windows;

namespace BookShop
{
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

        }

        private void Home(object sender, RoutedEventArgs e)
        {
            this.Hide();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

         
        }

        

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();


            About aboutWindow = new About();
            aboutWindow.Show();



            this.Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                CartWindow cartWindow = new CartWindow
                {
                    Owner = this, 
                    WindowStartupLocation = WindowStartupLocation.CenterOwner 
                };
                cartWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening CartWindow: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
