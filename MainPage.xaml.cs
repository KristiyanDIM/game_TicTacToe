
using Microsoft.Maui.Controls;

namespace game 
{

    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }


        private void OnLoginClicked(object sender, EventArgs e)
        {
            // Логика за логване
            DisplayAlert("Login", "Login functionality here", "OK");
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            // Логика за регистрация
            DisplayAlert("Register", "Registration functionality here", "OK");
        }
    }
}