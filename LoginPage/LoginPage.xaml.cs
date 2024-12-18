using Dapper;
using System.Text;
using Microsoft.Maui.Controls;
using System.Security.Cryptography;

namespace game
{
    public partial class LoginPage : ContentPage
    {
        private readonly DatabaseService _dbService = new DatabaseService();

        public LoginPage()
        {
            InitializeComponent();
        }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text?.Trim();
            var password = PasswordEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorLabel.Text = "Please fill in all fields.";
                ErrorLabel.IsVisible = true;
                return;
            }

            try
            {
                using var connection = _dbService.CreateConnection();
                var user = await connection.QueryFirstOrDefaultAsync<User>(
                    "SELECT * FROM Users WHERE Username = @Username",
                    new { Username = username });

                if (user == null || user.PasswordHash != HashPassword(password))
                {
                    ErrorLabel.Text = "Invalid username or password.";
                    ErrorLabel.IsVisible = true;
                    return;
                }

                await DisplayAlert("Success", $"Welcome, {user.Username}!", "OK");
                await Navigation.PushAsync(new TicTacToePage(user));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Логика за регистрация
            await Navigation.PushAsync(new RegisterPage());
        }

        public class User
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string PasswordHash { get; set; }
            public int Score { get; set; }
        }

    }
}
