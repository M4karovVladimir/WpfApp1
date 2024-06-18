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
using Microsoft.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private SqlConnection conn;
        public Window2()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Music; Integrated Security = True;"); conn.Open();
        }
        private void Enter_1(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select IdUSers from Users where user = '" +Login.Text+ "' and pasword = '" +Password.Text+ "';",conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var id = reader[0].ToString();
                reader.Close();
                Window3 window3 = new Window3(conn, id);
                window3.Show();
                this.Close();
            }
        }
       
    }
}
