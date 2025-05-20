using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using TenderApp.Services;
using TenderApp.Views;

using Microsoft.Extensions.DependencyInjection;

namespace TenderApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string _login = "Admin";

        [ObservableProperty]
        private int _attemptsCount = 3;

        public LoginViewModel(IAuthService authService)
            => _authService = authService;

        [RelayCommand]
        private void Apply(PasswordBox passwordBox)
        {
            if (_authService.IsLoginValid(Login, passwordBox.Password))
            {
                App.Services.GetRequiredService<TenderListWindow>().Show();
                Application.Current.Windows
                    .OfType<LoginWindow>().First().Close();
            }
            else
            {
                if(--AttemptsCount == 0)
                {
                    MessageBox.Show("Число возможных попыток исчерпано");
                    Application.Current.Shutdown();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
        }
    }
}
