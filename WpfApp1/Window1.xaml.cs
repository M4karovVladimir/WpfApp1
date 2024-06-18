using Microsoft.Data.SqlClient;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class Window1 : Window
    {
        private SqlConnection conn;
        private string IdUsers;

        public Window1(SqlConnection connection, string idUsers)
        {
            InitializeComponent();
            conn = connection;
            IdUsers = idUsers;
        }

        private void Add_New_Description(object sender, RoutedEventArgs e)
        {
            string description = DescriptionTextBox.Text;

            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Описание не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                conn.Open();
                string query = "INSERT INTO Songs (descriptionSongs) VALUES (@description)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@description", description);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Описание успешно добавлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Произошла ошибка при добавлении описания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
