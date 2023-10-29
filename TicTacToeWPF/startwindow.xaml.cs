using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Логика взаимодействия для startwindow.xaml
    /// </summary>
    public partial class startwindow : Window
    {
        
        
        public startwindow()
        {
            
            InitializeComponent();
        }

        private void button_start_Click(object sender, RoutedEventArgs e)
        {
            loginwindow mw = new loginwindow();
            mw.Show();
            this.Hide();
            
        }

        private void button_result_Click(object sender, RoutedEventArgs e)
        {
            resultswindow rw = new resultswindow();
            rw.Show();
            this.Hide();
        }

        private void close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
