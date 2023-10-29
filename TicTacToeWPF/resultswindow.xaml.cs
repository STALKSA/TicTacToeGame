using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

namespace TicTacToeWPF
{
    /// <summary>
    /// Логика взаимодействия для resultswindow.xaml
    /// </summary>
    public partial class resultswindow : Window
    {
        public resultswindow()
        {
            InitializeComponent();
            SQLiteConnection sqlite = new SQLiteConnection("Data Source=tictactoedb.db; Version=3;");

            SQLiteCommand sqlcmd;
            sqlite.Open();
            sqlcmd = sqlite.CreateCommand();
            string query = "SELECT * FROM Sessions";
            sqlcmd.CommandText = query;
            SQLiteDataAdapter sda = new SQLiteDataAdapter(sqlcmd);
            DataTable dt = new DataTable("Sessions");
            sda.Fill(dt);
            DataGridHome.ItemsSource = dt.DefaultView;
            sqlite.Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            startwindow sw = new startwindow();
            sw.Show();
            this.Hide();
        }

        private void closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
