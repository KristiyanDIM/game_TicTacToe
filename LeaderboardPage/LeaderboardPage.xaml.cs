using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace game
{
    public partial class LeaderboardPage : ContentPage
    {
        private readonly DatabaseService _dbService = new DatabaseService();

        public LeaderboardPage(game.LoginPage.User user)
        {
            InitializeComponent();
            LoadLeaderboard();
        }

        private async void LoadLeaderboard()
        {
            try
            {
                var leaderboard = await _dbService.GetLeaderboardAsync();
                LeaderboardListView.ItemsSource = leaderboard;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
