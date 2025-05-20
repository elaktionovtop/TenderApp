using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace TenderApp.ViewModels
{
    public partial class UserItemWindow : Window
    {
        public UserItemWindow(UserItemViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void FirstField_Loaded(object sender, 
            RoutedEventArgs e)
        {
            FirstField.Focus();
            FirstField.SelectAll();
        }

        private void ExecuteButton_Click(object sender,
            RoutedEventArgs e)
        {
            // Принудительное обновление binding'а у текущего элемента
            var element = FocusManager.GetFocusedElement(this) 
                as FrameworkElement;
            if(element != null)
            {
                var binding = BindingOperations.GetBindingExpression
                    (element, TextBox.TextProperty);
                binding?.UpdateSource();
            }
        }
    }
}
