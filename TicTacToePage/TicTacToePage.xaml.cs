using System;
using Microsoft.Maui.Controls;

namespace game
{
    public partial class TicTacToePage : ContentPage
    {
        private string currentPlayer = "X"; // �������� ����� (X ��� O)
        private string[,] board = new string[3, 3]; // ��������� ���� (3x3)
        private int moveCount = 0; // ����� �� ��������

        // ��������� �� ��������
        private int playerXScore = 0;
        private int playerOScore = 0;

        public TicTacToePage(LoginPage.User user)
        {
            InitializeComponent();
            InitializeBoard();
            UpdateScoreLabel();
            UpdateCurrentPlayerLabel();
        }

        // �������������� �� ��������� ����
        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = string.Empty; // ������ ��������� � ��������
                }
            }
            moveCount = 0;
        }

        // ��������� �������� ����� ������
        private async void OnCellTapped(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var coordinates = button.CommandParameter.ToString().Split(',');
            int row = int.Parse(coordinates[0]);
            int col = int.Parse(coordinates[1]);

            // ��� �������� ���� � �����, �����
            if (!string.IsNullOrEmpty(board[row, col]))
                return;

            // ��������� ���� �� ������� �����
            board[row, col] = currentPlayer;
            button.Text = currentPlayer;
            moveCount++;

            // �������� �� ���������
            if (CheckWinner())
            {
                if (currentPlayer == "X")
                {
                    playerXScore++; // ����������� ��������� �� ����� X
                }
                else
                {
                    playerOScore++; // ����������� ��������� �� ����� O
                }

                UpdateScoreLabel(); // ������������� �� ��������� � UI

                await DisplayAlert("���������", $"����� {currentPlayer} ������!", "OK");
                RestartGame();
                return;
            }

            // �������� �� ���������
            if (moveCount == 9)
            {
                await DisplayAlert("���������", "���� ���������!", "OK");
                RestartGame();
                return;
            }

            // ����� �� ������
            currentPlayer = currentPlayer == "X" ? "O" : "X";
            UpdateCurrentPlayerLabel();
        }

        // ��������� �� ���������
        private bool CheckWinner()
        {
            // �������� �� �������� � ��������
            for (int i = 0; i < 3; i++)
            {
                if ((board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer) ||
                    (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer))
                {
                    return true;
                }
            }

            // �������� �� �����������
            if ((board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) ||
                (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer))
            {
                return true;
            }

            return false;
        }

        // ����������� ������ �� ������� �� ������� �����
        private void UpdateCurrentPlayerLabel()
        {
            CurrentPlayerLabel.Text = $"����� {currentPlayer} � �� ���";
        }

        // ����������� ������ �� ������� �� ���������
        private void UpdateScoreLabel()
        {
            ScoreLabel.Text = $"�������� - ����� X: {playerXScore} | ����� O: {playerOScore}";
        }

        // ���������� ������
        private void RestartGame()
        {
            InitializeBoard();
            foreach (var child in grid.Children)
            {
                if (child is Button button)
                {
                    button.Text = string.Empty; // ���������� ������ �� ��������
                }
            }

            currentPlayer = "X"; // ��������� ������ � ����� X
            UpdateCurrentPlayerLabel();
        }

        // ��������� �� ��������� �� ������ �� �������
        private void OnRestartClicked(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}
