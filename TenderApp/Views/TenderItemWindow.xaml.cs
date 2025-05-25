using System.Windows;

using TenderApp.Models;
using TenderApp.ViewModels;

namespace TenderApp.Views
{
    public partial class TenderItemWindow : Window
    {
        public TenderItemWindow(ItemViewModel<Tender> viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void FirstField_Loaded(object sender, 
            RoutedEventArgs e)
        {
            ItemWindowHelper.FocusFirstField(sender);
        }
    }
}
