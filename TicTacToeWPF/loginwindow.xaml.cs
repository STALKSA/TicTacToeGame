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

namespace TicTacToeWPF
{
    /// <summary>
    /// Логика взаимодействия для loginwindow.xaml
    /// </summary>
    public partial class loginwindow : Window
    {
        public loginwindow()
        {
            InitializeComponent();
        }
        Constants c = new Constants();
        private void button_session_Click(object sender, RoutedEventArgs e)
        {
            Constants.Session = tb_session.Text;
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Hide();
        }

        private void close(object sender, System.ComponentModel.CancelEventArgs e)
        {

            System.Windows.Application.Current.Shutdown();
        }
    }
}
