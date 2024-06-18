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
    public partial class Window3 : Window
    {
        private SqlConnection conn;
        private string IdUsers;

        public Window3(SqlConnection connection, string idUsers)
        {
            InitializeComponent();
            conn = connection;
            IdUsers = idUsers;
            Change_Songs();
        }

        private void Change_Songs()
        {
            ListBox.Items.Clear();
            SqlCommand cmd = new SqlCommand("SELECT IdSongs, nameSongs FROM Songs", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string songName = reader.GetString(1);
                AddSongs(songName);
            }

            reader.Close();
        }

        private void AddSongs(string text)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = text,
                Margin = new Thickness(5)
            };
            ListBox.Items.Add(textBlock);
        }

        private void SongsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox.SelectedItem != null)
            {
                var selectedTextBlock = (TextBlock)ListBox.SelectedItem;
                string selectedSongName = selectedTextBlock.Text;

                SqlCommand cmd = new SqlCommand("SELECT descriptionSongs FROM Songs WHERE nameSongs = @nameSongs", conn);
                cmd.Parameters.AddWithValue("@nameSongs", selectedSongName);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string description = reader.GetString(0);
                    AddTextBlock(Stac, description); 
                }

                reader.Close();
            }
        }

        private void DesriptionCheck()
        {
            Stac.Children.Clear();
            if (ListBox.SelectedItem != null)
            {
                string songName = ((TextBlock)ListBox.SelectedItem).Text;
                SqlCommand cmd = new SqlCommand("SELECT descriptionSongs FROM Songs WHERE nameSongs = @nameSongs", conn);
                cmd.Parameters.AddWithValue("@nameSongs", songName);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string descriptionSong = reader.GetString(0);
                    AddTextBlock(Stac, "Описание: " + descriptionSong);
                }

                reader.Close();
            }
        }

        private void AddTextBlock(StackPanel stackPanel, string text)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = text,
                Margin = new Thickness(5)
            };
            stackPanel.Children.Add(textBlock);
        }

        private TextBlock CreateTextBlock(string text)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = text,
                FontSize = 16,
                Margin = new Thickness(5)
            };
            return textBlock;
        }

        private void Add_Song_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1(conn, IdUsers);
            window1.Show();
            this.Close();
        }

    }
}