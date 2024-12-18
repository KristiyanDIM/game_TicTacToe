
using Microsoft.Maui.Controls;

namespace game 
{

    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }


        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Логика за логване
            await Navigation.PushAsync(new LoginPage());
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Логика за регистрация
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}