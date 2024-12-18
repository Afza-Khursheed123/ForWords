using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using BookShop;
using MySql.Data.MySqlClient;
using static FORWORDS.MainWindow;

namespace FORWORDS
{
    public partial class MainWindow : Window
    {
        private Book[] AllBooks;
        private Trie bookTrie;
        private string connectionString = "Server=localhost;Database=books_store;Uid=root;Pwd=Afza23001!!!;";
        public Book[] Books { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            bookTrie = new Trie();
            LoadBooks();
        }

        public class TrieNode
        {
            public Dictionary<char, TrieNode> Children { get; set; }
            public bool IsEndOfWord { get; set; }
            public int BookIndex { get; set; }

            public TrieNode()
            {
                Children = new Dictionary<char, TrieNode>();
                IsEndOfWord = false;
                BookIndex = -1;
            }
        }

        public class Trie
        {
            private TrieNode root;

            public Trie()
            {
                root = new TrieNode();
            }

      
            public void Insert(string word, int index)
            {
                TrieNode current = root;
                foreach (char c in word.ToLower()) 
                {
                    if (!current.Children.ContainsKey(c))
                    {
                        current.Children[c] = new TrieNode();
                    }
                    current = current.Children[c];
                }
                current.IsEndOfWord = true;
                current.BookIndex = index;
            }

      //searching
            public int Search(string word)
            {
                TrieNode current = root;
                foreach (char c in word.ToLower()) 
                {
                    if (!current.Children.ContainsKey(c))
                    {
                        return -1;
                    }
                    current = current.Children[c];
                }
                return current.IsEndOfWord ? current.BookIndex : -1;
            }

       
            public Dictionary<int, Book> SearchByPrefix(string prefix, Book[] books)
            {
                TrieNode current = root;
                Dictionary<int, Book> matchedBooks = new Dictionary<int, Book>();

   
                foreach (char c in prefix.ToLower()) 
                {
                    if (!current.Children.ContainsKey(c))
                    {
                        return matchedBooks;
                    }
                    current = current.Children[c];
                }

    
                FindWordsWithPrefix(current, ref matchedBooks, books);
                return matchedBooks;
            }

            private void FindWordsWithPrefix(TrieNode node, ref Dictionary<int, Book> matchedBooks, Book[] books)
            {
                if (node.IsEndOfWord)
                {
                    if (node.BookIndex >= 0 && node.BookIndex < books.Length)
                    {
                        matchedBooks[node.BookIndex] = books[node.BookIndex];
                    }
                }

                foreach (var child in node.Children.Values)
                {
                    FindWordsWithPrefix(child, ref matchedBooks, books);
                }
            }
        }

        public class Book
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public double Price { get; set; }
            public string ImagePath { get; set; }
            public double Rating { get; set; }
            public int Stock { get; set; }
        }

        private void LoadBooks()
        {
            string query = "SELECT TITLE, AUTHORNAME, PRICE, IMAGEURL, COALESCE(RATING, 0) AS RATING, COALESCE(STOCK, 0) AS STOCK FROM BOOK";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand countCommand = new MySqlCommand("SELECT COUNT(*) FROM BOOK", connection);
                    int rowCount = Convert.ToInt32(countCommand.ExecuteScalar());
                    Books = new Book[rowCount];
                    AllBooks = new Book[rowCount];
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int index = 0;
                        while (reader.Read())
                        {
                            string title = reader["TITLE"]?.ToString();
                            string author = reader["AUTHORNAME"]?.ToString();
                            double price = Convert.ToDouble(reader["PRICE"]);
                            string imagePath = reader["IMAGEURL"]?.ToString();
                            double rating = Convert.ToDouble(reader["RATING"]);
                            int stock = Convert.ToInt32(reader["STOCK"]);

                            if (stock > 0)
                            {
                                var book = new Book
                                {
                                    Title = title,
                                    Author = author,
                                    Price = price,
                                    ImagePath = imagePath,
                                    Rating = rating,
                                    Stock = stock
                                };
                                Books[index] = book;
                                AllBooks[index] = book;
                                bookTrie.Insert(title, index);
                                index++;
                            }
                        }
                    }
                }

          
                DisplayBooks();
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show($"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        private void DisplayBooks()
        {
            try
            {
                DataContext = null; 
                DataContext = this; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying books: {ex.Message}");
            }
        }


        //sorting
        public void MergeSortHL(Book[] books)
        {
            if (books == null || books.Length <= 1)
                return;

            Book[] tempArray = new Book[books.Length];
            MergeSortD(books, tempArray, 0, books.Length - 1);
        }

        private void MergeSortD(Book[] books, Book[] tempArray, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                MergeSortD(books, tempArray, left, middle);
                MergeSortD(books, tempArray, middle + 1, right);
                MergeD(books, tempArray, left, middle, right);
            }
        }

        private void MergeD(Book[] books, Book[] tempArray, int left, int middle, int right)
        {
            int i = left;
            int j = middle + 1;
            int k = left;

            while (i <= middle && j <= right)
            {
                if (books[i].Price >= books[j].Price)
                {
                    tempArray[k] = books[i];
                    i++;
                }
                else
                {
                    tempArray[k] = books[j];
                    j++;
                }
                k++;
            }

            while (i <= middle)
            {
                tempArray[k] = books[i];
                i++;
                k++;
            }

            while (j <= right)
            {
                tempArray[k] = books[j];
                j++;
                k++;
            }

            for (i = left; i <= right; i++)
            {
                books[i] = tempArray[i];
            }
        }

        public void MergeSortLH(Book[] books)
        {
            if (books == null || books.Length <= 1)
                return;

            Book[] tempArray = new Book[books.Length];
            MergeSortA(books, tempArray, 0, books.Length - 1);
        }

        private void MergeSortA(Book[] books, Book[] tempArray, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                MergeSortA(books, tempArray, left, middle);
                MergeSortA(books, tempArray, middle + 1, right);
                MergeA(books, tempArray, left, middle, right);
            }
        }

        private void MergeA(Book[] books, Book[] tempArray, int left, int middle, int right)
        {
            int i = left;
            int j = middle + 1;
            int k = left;

            while (i <= middle && j <= right)
            {
                if (books[i].Price <= books[j].Price)
                {
                    tempArray[k] = books[i];
                    i++;
                }
                else
                {
                    tempArray[k] = books[j];
                    j++;
                }
                k++;
            }

            while (i <= middle)
            {
                tempArray[k] = books[i];
                i++;
                k++;
            }

            while (j <= right)
            {
                tempArray[k] = books[j];
                j++;
                k++;
            }

            for (i = left; i <= right; i++)
            {
                books[i] = tempArray[i];
            }
        }


    
        private void sortLH(object sender, RoutedEventArgs e)
        {
            MergeSortLH(Books);
            DisplayBooks();
        }

  
        private void sortHL(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Sorting High to Low");
            MergeSortHL(Books);
            DisplayBooks();
        }
    
    private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox?.Text))
            {
                MessageBox.Show("Please enter a search term.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string searchText = textBox.Text.ToLower(); 

            try
            {
                Dictionary<int, Book> matchingBooks = bookTrie.SearchByPrefix(searchText, Books);

                if (matchingBooks.Count > 0)
                {
                 
                    Books = matchingBooks.Values.ToArray();
                    DataContext = null;
                    DataContext = this;
                }
                else
                {
                    MessageBox.Show("No books found matching the search criteria.", "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while searching: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Image_Click(object sender, MouseButtonEventArgs e)
        {
  
            if (sender is Image image && image.DataContext is Book selectedBook)
            {
            
                Page2 page2Window = new Page2(selectedBook);

                page2Window.Owner = this; 
                page2Window.Show();       
                this.Hide();             
            }
            else
            {
                MessageBox.Show("Failed to load the selected book.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
        
                SearchButton_Click(sender, e);
            }
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {

            Books = AllBooks;

            DataContext = null;
            DataContext = this; 

        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Book selectedBook = (Book)((Image)sender).DataContext; 

          
            Page2 page2Window = new Page2(selectedBook);
            page2Window.Owner = this;
            page2Window.Show();
            this.Hide(); 
        }

        private void Page2_Closed(object sender, EventArgs e)
        {
            this.Owner.Show();  
        }


        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var cartWindow = new CartWindow();
            cartWindow.Show();
            this.Close();
        }

        private void textBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void textBox_TextChanged_1(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void Home(object sender, RoutedEventArgs e)
        {

        }

        private void About(object sender, RoutedEventArgs e)
        {
            this.Hide();

        
            About aboutWindow = new About();
            aboutWindow.Show();


         
            this.Close();
        }
    }
}



