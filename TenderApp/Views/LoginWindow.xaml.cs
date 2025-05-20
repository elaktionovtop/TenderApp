using TenderApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TenderApp.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext 
                = App.Services.GetRequiredService<LoginViewModel>();
        }
        private void FirstBox_Loaded(object sender, RoutedEventArgs e)
        {
            FirstBox.Focus();
            FirstBox.SelectAll();
        }
    }
}
