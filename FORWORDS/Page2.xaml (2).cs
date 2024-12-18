using static FORWORDS.MainWindow;
using System.Windows.Input;
using System.Windows;
using static System.Reflection.Metadata.BlobBuilder;
using BookShop;
using System.Diagnostics;
using MySql.Data.MySqlClient;
namespace FORWORDS;
public partial class Page2 : Window  
{
    private string connectionString = "Server=localhost;Database=s_cart;Uid=root;Pwd=Afza23001!!!;";

    public Page2(Book selectedBook)
    {
        InitializeComponent();
        Debug.WriteLine($"Title: {selectedBook.Title}, Price: {selectedBook.Price}");

        this.DataContext = selectedBook;  
    }


    private void back(object sender, RoutedEventArgs e)
    {
        if (this.Owner is MainWindow mainWindow)
        {
            mainWindow.Show();
            this.Close();     
        }
        else
        {
            MessageBox.Show("MainWindow reference is missing.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }



    private void button3_Click(object sender, RoutedEventArgs e)
    {
        CartWindow cartWindow = new CartWindow
        {
            Owner = this,            
            WindowStartupLocation = WindowStartupLocation.CenterOwner 
        };
        cartWindow.Show(); 
    }

    private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
           
        }
    }

    private void Addto_Click(object sender, RoutedEventArgs e)
    {
        Book selectedBook = this.DataContext as Book;
        if (selectedBook != null)
        {
            string imagePath = selectedBook.ImagePath; 

            decimal price = Convert.ToDecimal(selectedBook.Price);

            InsertIntoCart(selectedBook.Title, price, imagePath);
        }
    }
    private void InsertIntoCart(string title, decimal price, string imageUrl)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO sCart_details (Title, Price, IMAGEURL) VALUES (@Title, @Price, @ImageUrl)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@ImageUrl", imageUrl);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Debug.WriteLine($"Rows Affected: {rowsAffected}");

                    if (rowsAffected > 0)
                    {
                        string verifyQuery = "SELECT COUNT(*) FROM sCart_details WHERE Title = @Title AND Price = @Price AND IMAGEURL = @ImageUrl";
                        using (MySqlCommand verifyCmd = new MySqlCommand(verifyQuery, conn))
                        {
                            verifyCmd.Parameters.AddWithValue("@Title", title);
                            verifyCmd.Parameters.AddWithValue("@Price", price);
                            verifyCmd.Parameters.AddWithValue("@ImageUrl", imageUrl);

                            int count = Convert.ToInt32(verifyCmd.ExecuteScalar());
                            if (count > 0)
                            {
                                MessageBox.Show("Item added to cart successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Verification failed. Item not found in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to add item to cart.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
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
