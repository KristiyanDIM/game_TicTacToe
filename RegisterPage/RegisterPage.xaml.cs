using Dapper;
using System.Security.Cryptography;
using System.Text;

namespace game
{

    public partial class RegisterPage : ContentPage
    {
        private readonly DatabaseService _dbService = new DatabaseService();

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
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
                var passwordHash = HashPassword(password);

                await connection.ExecuteAsync(
                    "INSERT INTO Users (Username, PasswordHash, Score) VALUES (@Username, @PasswordHash, 0)",
                    new { Username = username, PasswordHash = passwordHash });

                await DisplayAlert("Success", "Registration successful!", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = ex.Message;
                ErrorLabel.IsVisible = true;
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
