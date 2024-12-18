using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;

namespace game
{
    public partial class TicTacToePage : ContentPage
    {
        private string currentPlayer = "X";
        private string[,] board = new string[3, 3]; // 3x3 масив за игралното поле
        private int moveCount = 0; // Броя на направените ходове

        public TicTacToePage(LoginPage.User user)
        {
            InitializeComponent();
            InitializeBoard();

        }

        // Инициализиране на игралното поле
        private void InitializeBoard()
        {
            // Запълваме всички клетки с празни низове
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = string.Empty;
                }
            }
            moveCount = 0;
        }

        // Метод за обработка на кликване върху клетка
        private void OnCellTapped(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var coordinates = button.CommandParameter.ToString().Split(',');
            int row = int.Parse(coordinates[0]);
            int col = int.Parse(coordinates[1]);

            // Ако клетката вече е заета, изход
            if (!string.IsNullOrEmpty(board[row, col]))
                return;

            // Поставяме символа на текущия играч в съответната клетка
            board[row, col] = currentPlayer;
            button.Text = currentPlayer;
            moveCount++;

            // Проверяваме дали има победител
            if (CheckWinner())
            {
                DisplayAlert("Победител", $"Играч {currentPlayer} победи!", "OK");
                RestartGame();
                return;
            }

            // Проверяваме за равенство
            if (moveCount == 9)
            {
                DisplayAlert("Равенство", "Няма победител!", "OK");
                RestartGame();
                return;
            }

            // Превключваме на следващия играч
            currentPlayer = currentPlayer == "X" ? "O" : "X";
            CurrentPlayerLabel.Text = $"Играч {currentPlayer} е на ход";
        }

        // Метод за проверка на победител
        private bool CheckWinner()
        {
            // Проверяваме редовете и колоните
            for (int i = 0; i < 3; i++)
            {
                if ((board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer) ||
                    (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer))
                {
                    return true;
                }
            }

            // Проверка на диагоналите
            if ((board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) ||
                (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer))
            {
                return true;
            }

            return false;
        }

        private void RestartGame()
        {
            // Изчистване на игралното поле
            InitializeBoard();

            // Изчистваме текста на всеки бутон в игралното поле
            foreach (var child in grid.Children)
            {
                if (child is Button button)
                {
                    button.Text = string.Empty; // Изчистваме текста на бутона
                }
            }

            // Връщаме играча X на ход
            currentPlayer = "X";
            CurrentPlayerLabel.Text = "Играч X е на ход";
        }



        // Метод за рестартиране на играта (използваме го и при равенство)
        private void OnRestartClicked(object sender, EventArgs e)
        {
            RestartGame();
        }


    }
}
