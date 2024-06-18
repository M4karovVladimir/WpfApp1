using Microsoft.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private SqlConnection conn;

        public MainWindow()
        {
            InitializeComponent();
            conn = new SqlConnection("your_connection_string_here");
            conn.Open();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Text;

            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Users (Login, Password) VALUES (@Login, @Password)", conn);
                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.ExecuteNonQuery();

                  
                    Window2 window2 = new Window2();
                    window2.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Логин и пароль не могут быть пустыми.");
            }
        }
    }
}