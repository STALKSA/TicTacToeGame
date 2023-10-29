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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TicTacToeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // true = X / false = 0
        public static bool TURN = true;
        // true = hot seat / false = AI
        public static int MODE = Constants.HOT_SEAT_MODE;

        private List<Button> gameButtons;

        ApplicationContext db;
        
        SoundPlayer playSound = new SoundPlayer(Properties.Resources.background);
        public MainWindow()
        {
            
            playSound.PlayLooping();
            InitializeComponent();

            db = new ApplicationContext();
            gameButtons = new List<Button>();
            
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dtTicker;
            dt.Start();

            gameButtons.Add(A1);
            gameButtons.Add(A2);
            gameButtons.Add(A3);
            gameButtons.Add(A4);
            gameButtons.Add(A5);

            gameButtons.Add(B1);
            gameButtons.Add(B2);
            gameButtons.Add(B3);
            gameButtons.Add(B4);
            gameButtons.Add(B5);

            gameButtons.Add(C1);
            gameButtons.Add(C2);
            gameButtons.Add(C3);
            gameButtons.Add(C4);
            gameButtons.Add(C5);

            gameButtons.Add(D1);
            gameButtons.Add(D2);
            gameButtons.Add(D3);
            gameButtons.Add(D4);
            gameButtons.Add(D5);

            gameButtons.Add(F1);
            gameButtons.Add(F2);
            gameButtons.Add(F3);
            gameButtons.Add(F4);
            gameButtons.Add(F5);
            
        }
        private int increment = 0;
        private int m = 0;
        private void dtTicker(object sender, EventArgs e)
        {
            increment++;
            if (increment < 59)
            {
                if (increment < 10)
                    timersec.Content = "0" + Convert.ToString(increment);
                else
                    timersec.Content = Convert.ToString(increment);
            }
            else
            {
                increment = 0;
                m++;
                if (m < 10)
                    timermin.Content = "0" + Convert.ToString(m);
                else
                    timermin.Content = Convert.ToString(m);
            }
        }

        private void gameAction_Click(object sender, RoutedEventArgs e)
        {
            // Make move
            Button pressedButton = (Button)sender;
            if(TURN) {
                pressedButton.Content = Constants.X_SYMBOL;
                pressedButton.IsEnabled = false;
                TURN = false;
            } else {
                pressedButton.Content = Constants.O_SYMBOL;
                pressedButton.IsEnabled = false;
                TURN = true;
            }

            if (checkGameStatus())
            {
                return;
            }

            // Move AI if necessary
            if (MODE == Constants.AI_EASY_MODE || MODE == Constants.AI_HARD_MODE)
            {
                performAiMove();
                TURN = true;
            }

            checkGameStatus();
        }

        

        private void onRestartButton_Click(object sender, EventArgs e)
        {
            // Restart the game
            // Activate all buttons and reset their texts
            foreach(Button button in gameButtons)
            {
                button.IsEnabled = true;
                button.Content = "";
            }

            // Reset member variables
            TURN = true;
        }

        private void gameModeComboBox_Click(object sender, SelectionChangedEventArgs e)
        {
            // Get clicked item
            int selection = gameModeComboBox.SelectedIndex;
            MODE = selection;
        }

        private void performAiMove()
        {
            if (MODE == Constants.AI_EASY_MODE)
            {
                ArtificialIntelligence.performEasyMove(gameButtons);
            }
            else if (MODE == Constants.AI_HARD_MODE)
            {
                ArtificialIntelligence.performHardMove(gameButtons);
            }
        }

        private bool checkGameStatus()
        {
            GameStatus status = checkHorizontal();
            if (status.isGameOver())
            {
                disableGame();
                updateStats(status);
                MessageBox.Show("Player " + status.winner + " has won the game!");
                return true;
            }

            status = checkVertical();
            if (status.isGameOver())
            {
                disableGame();
                updateStats(status);
                MessageBox.Show("Player " + status.winner + " has won the game!");
                return true;
            }

            status = checkDiagonal();
            if (status.isGameOver())
            {
                disableGame();
                updateStats(status);
                MessageBox.Show("Player " + status.winner + " has won the game!");
                return true;
            }

            if (checkForTie())
            {
                disableGame();
                updateStats(new GameStatus(true, "", true));
                MessageBox.Show("The game ended in a tie!");
                return true;
            }

            return false;
        }

        private void disableGame()
        {
            foreach(Button button in gameButtons)
            {
                button.IsEnabled = false;
            }
        }

        private void updateStats(GameStatus status)
        {
            if (status.isGameOver())
            {
                if(status.getWinner().Equals(Constants.X_SYMBOL))
                {
                    int currentWins = Convert.ToInt32(winsX.Content);
                    winsX.Content = "" + (currentWins + 1);
                }
                else if (status.getWinner().Equals(Constants.O_SYMBOL))
                {
                    int currentWins = Convert.ToInt32(winsO.Content);
                    winsO.Content = "" + (currentWins + 1);
                }
                else if (status.isTie())
                {
                    int currentTies = Convert.ToInt32(ties.Content);
                    ties.Content = "" + (currentTies + 1);
                }
            }
        }

        // HELPER
        private GameStatus checkHorizontal()
        {
            bool gameOver = false;
            string winner = "";
            // Horizontal check
            if (A1.Content.Equals(A2.Content)
                    && A1.Content.Equals(A3.Content)
                    && A2.Content.Equals(A3.Content)
                    && A3.Content.Equals(A4.Content)
                    && A4.Content.Equals(A5.Content)
                    && A2.Content.Equals(A4.Content)
                    && A2.Content.Equals(A5.Content)
                    && A1.Content.Equals(A4.Content)
                    && A1.Content.Equals(A5.Content)
                    && A1.Content.Equals(A2.Content)
                    && !A1.Content.Equals(""))
            {
                // Bottom row won
                //MessageBox.Show("bottom row");
                gameOver = true;
                winner = Convert.ToString(A1.Content);
            }
            else if (B1.Content.Equals(B2.Content)
                    && B1.Content.Equals(B3.Content)
                    && B2.Content.Equals(B3.Content)
                    && B3.Content.Equals(B4.Content)
                    && B4.Content.Equals(B5.Content)
                    && B2.Content.Equals(B4.Content)
                    && B2.Content.Equals(B5.Content)
                    && B1.Content.Equals(B4.Content)
                    && B1.Content.Equals(B5.Content)
                    && B1.Content.Equals(B2.Content)
                    && !B1.Content.Equals(""))
            {
                // Bottom row won
                //MessageBox.Show("bottom row");
                gameOver = true;
                winner = Convert.ToString(B1.Content);
            }
            else if (C1.Content.Equals(C2.Content)
                    && C1.Content.Equals(C3.Content)
                    && C2.Content.Equals(C3.Content)
                    && C3.Content.Equals(C4.Content)
                    && C4.Content.Equals(C5.Content)
                    && C2.Content.Equals(C4.Content)
                    && C2.Content.Equals(C5.Content)
                    && C1.Content.Equals(C4.Content)
                    && C1.Content.Equals(C5.Content)
                    && C1.Content.Equals(C2.Content)
                    && !C1.Content.Equals(""))
            {
                // Bottom row won
                //MessageBox.Show("bottom row");
                gameOver = true;
                winner = Convert.ToString(C1.Content);
            }
            else if (F1.Content.Equals(F2.Content)
                    && F1.Content.Equals(F3.Content)
                    && F2.Content.Equals(F3.Content)
                    && F3.Content.Equals(F4.Content)
                    && F4.Content.Equals(F5.Content)
                    && F2.Content.Equals(F4.Content)
                    && F2.Content.Equals(F5.Content)
                    && F1.Content.Equals(F4.Content)
                    && F1.Content.Equals(F5.Content)
                    && F1.Content.Equals(F2.Content)
                    && !F1.Content.Equals(""))
            {
                // Bottom row won
                //MessageBox.Show("bottom row");
                gameOver = true;
                winner = Convert.ToString(F1.Content);
            }
            else if (D1.Content.Equals(D2.Content)
                    && D1.Content.Equals(D3.Content)
                    && D2.Content.Equals(D3.Content)
                    && D3.Content.Equals(D4.Content)
                    && D4.Content.Equals(D5.Content)
                    && D2.Content.Equals(D4.Content)
                    && D2.Content.Equals(D5.Content)
                    && D1.Content.Equals(D4.Content)
                    && D1.Content.Equals(D5.Content)
                    && D1.Content.Equals(D2.Content)
                    && !D1.Content.Equals(""))
            {
                // Bottom row won
                //MessageBox.Show("bottom row");
                gameOver = true;
                winner = Convert.ToString(D1.Content);
            }

            return new GameStatus(gameOver, winner, false);
        }

        private GameStatus checkVertical()
        {
            bool gameOver = false;
            string winner = "";
            // Vertical check
            if (A1.Content.Equals(B1.Content)
                && A1.Content.Equals(C1.Content)
                && A1.Content.Equals(D1.Content)
                && A1.Content.Equals(F1.Content)
                && B1.Content.Equals(D1.Content)
                && B1.Content.Equals(F1.Content)
                && D1.Content.Equals(F1.Content)
                && !A1.Content.Equals(""))
            {
                // Left column won
                //MessageBox.Show("left column");
                gameOver = true;
                winner = Convert.ToString(A1.Content);
            }
            else if (A2.Content.Equals(B2.Content)
                && A2.Content.Equals(C2.Content)
                && A2.Content.Equals(D2.Content)
                && A2.Content.Equals(F2.Content)
                && B2.Content.Equals(D2.Content)
                && B2.Content.Equals(F2.Content)
                && D2.Content.Equals(F2.Content)
                && !A2.Content.Equals(""))
            {
                // Left column won
                //MessageBox.Show("left column");
                gameOver = true;
                winner = Convert.ToString(A2.Content);
            }
            if (A3.Content.Equals(B3.Content)
                && A3.Content.Equals(C3.Content)
                && A3.Content.Equals(D3.Content)
                && A3.Content.Equals(F3.Content)
                && B3.Content.Equals(D3.Content)
                && B3.Content.Equals(F3.Content)
                && D3.Content.Equals(F3.Content)
                && !A3.Content.Equals(""))
            {
                // Left column won
                //MessageBox.Show("left column");
                gameOver = true;
                winner = Convert.ToString(A3.Content);
            }
            if (A4.Content.Equals(B4.Content)
                && A4.Content.Equals(C4.Content)
                && A4.Content.Equals(D4.Content)
                && A4.Content.Equals(F4.Content)
                && B4.Content.Equals(D4.Content)
                && B4.Content.Equals(F4.Content)
                && D4.Content.Equals(F4.Content)
                && !A4.Content.Equals(""))
            {
                // Left column won
                //MessageBox.Show("left column");
                gameOver = true;
                winner = Convert.ToString(A4.Content);
            }
            if (A5.Content.Equals(B5.Content)
                && A5.Content.Equals(C5.Content)
                && A5.Content.Equals(D5.Content)
                && A5.Content.Equals(F5.Content)
                && B5.Content.Equals(D5.Content)
                && B5.Content.Equals(F5.Content)
                && D5.Content.Equals(F5.Content)
                && !A5.Content.Equals(""))
            {
                // Left column won
                //MessageBox.Show("left column");
                gameOver = true;
                winner = Convert.ToString(A5.Content);
            }

            return new GameStatus(gameOver, winner, false);
        }

        private GameStatus checkDiagonal()
        {
            bool gameOver = false;
            string winner = "";
            // Diagonal check
            if (A1.Content.Equals(B2.Content)
                && A1.Content.Equals(C3.Content)
                && A1.Content.Equals(D4.Content)
                && A1.Content.Equals(F5.Content)
                && B2.Content.Equals(C3.Content)
                && B2.Content.Equals(D4.Content)
                && B2.Content.Equals(F5.Content)
                && C3.Content.Equals(D4.Content)
                && C3.Content.Equals(F5.Content)
                && D4.Content.Equals(F5.Content)
                && !A1.Content.Equals(""))
            {
                // Top left to bottom right won
                //MessageBox.Show("tl-br");
                gameOver = true;
                winner = Convert.ToString(A1.Content);
            }
            else if (F1.Content.Equals(D2.Content)
                    && F1.Content.Equals(C3.Content)
                    && F1.Content.Equals(B4.Content)
                    && F1.Content.Equals(A5.Content)
                    && D2.Content.Equals(C3.Content)
                    && D2.Content.Equals(B4.Content)
                    && D2.Content.Equals(A5.Content)
                    && C3.Content.Equals(B4.Content)
                    && C3.Content.Equals(A5.Content)
                    && B4.Content.Equals(A5.Content)

                    && !F1.Content.Equals(""))
            {
                // Bottom left to top right won
                //MessageBox.Show("bl-tr");
                gameOver = true;
                winner = Convert.ToString(F1.Content);
            }

            return new GameStatus(gameOver, winner, false);
        }

        private bool checkForTie()
        {
            bool tie = true;
            foreach (Button button in gameButtons)
            {
                if (button.IsEnabled == true)
                {
                    tie = false;
                    break;
                }
            }

            return tie;
        }

        

        private void l(object sender, RoutedEventArgs e)
        {
            
            label_session.Content = Constants.Session;
        }

        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
            
            string time;
            string wx;
            string wo;
            wx = Convert.ToString(winsX.Content);
            wo = Convert.ToString(winsO.Content);
            time = Convert.ToString(timermin.Content) + " мин." + Convert.ToString(timersec.Content) + " сек.";

            sessions sessions = new sessions(Constants.Session, wx, wo, time);

            db.Sessions.Add(sessions);
            db.SaveChanges();
            startwindow SW = new startwindow();
            SW.Show();
            this.Hide();
            playSound.Stop();
        }
        DispatcherTimer  dt = new DispatcherTimer();

        private void close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            string time;
            string wx;
            string wo;
            wx = Convert.ToString(winsX.Content);
            wo = Convert.ToString(winsO.Content);
            time = Convert.ToString(timermin.Content) + " мин." + Convert.ToString(timersec.Content) + " сек.";

            sessions sessions = new sessions(Constants.Session, wx, wo, time);

            db.Sessions.Add(sessions);
            db.SaveChanges();
            playSound.Stop();
            System.Windows.Application.Current.Shutdown();
        }
    }
}
