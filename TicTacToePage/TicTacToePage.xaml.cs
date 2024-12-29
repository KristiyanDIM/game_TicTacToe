using System;
using Microsoft.Maui.Controls;

namespace game
{
    public partial class TicTacToePage : ContentPage
    {
        private string currentPlayer = "X"; // Текущият играч (X или O)
        private string[,] board = new string[3, 3]; // Игралното поле (3x3)
        private int moveCount = 0; // Брояч на ходовете

        // Резултати на играчите
        private int playerXScore = 0;
        private int playerOScore = 0;

        public TicTacToePage(LoginPage.User user)
        {
            InitializeComponent();
            InitializeBoard();
            UpdateScoreLabel();
            UpdateCurrentPlayerLabel();
        }

        // Инициализиране на игралното поле
        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = string.Empty; // Празни стойности в началото
                }
            }
            moveCount = 0;
        }

        // Обработва кликване върху клетка
        private async void OnCellTapped(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var coordinates = button.CommandParameter.ToString().Split(',');
            int row = int.Parse(coordinates[0]);
            int col = int.Parse(coordinates[1]);

            // Ако клетката вече е заета, изход
            if (!string.IsNullOrEmpty(board[row, col]))
                return;

            // Записваме хода на текущия играч
            board[row, col] = currentPlayer;
            button.Text = currentPlayer;
            moveCount++;

            // Проверка за победител
            if (CheckWinner())
            {
                if (currentPlayer == "X")
                {
                    playerXScore++; // Увеличаваме резултата на играч X
                }
                else
                {
                    playerOScore++; // Увеличаваме резултата на играч O
                }

                UpdateScoreLabel(); // Актуализиране на резултата в UI

                await DisplayAlert("Победител", $"Играч {currentPlayer} победи!", "OK");
                RestartGame();
                return;
            }

            // Проверка за равенство
            if (moveCount == 9)
            {
                await DisplayAlert("Равенство", "Няма победител!", "OK");
                RestartGame();
                return;
            }

            // Смяна на играча
            currentPlayer = currentPlayer == "X" ? "O" : "X";
            UpdateCurrentPlayerLabel();
        }

        // Проверява за победител
        private bool CheckWinner()
        {
            // Проверка на редовете и колоните
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

        // Актуализира текста на етикета за текущия играч
        private void UpdateCurrentPlayerLabel()
        {
            CurrentPlayerLabel.Text = $"Играч {currentPlayer} е на ход";
        }

        // Актуализира текста на етикета за резултата
        private void UpdateScoreLabel()
        {
            ScoreLabel.Text = $"Резултат - Играч X: {playerXScore} | Играч O: {playerOScore}";
        }

        // Рестартира играта
        private void RestartGame()
        {
            InitializeBoard();
            foreach (var child in grid.Children)
            {
                if (child is Button button)
                {
                    button.Text = string.Empty; // Изчистваме текста на бутоните
                }
            }

            currentPlayer = "X"; // Започваме отново с играч X
            UpdateCurrentPlayerLabel();
        }

        // Обработка на натискане на бутона за рестарт
        private void OnRestartClicked(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}
