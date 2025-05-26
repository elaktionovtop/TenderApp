using System.Windows;

using TenderApp.Models;
using TenderApp.ViewModels;

namespace TenderApp.Views
{
    public partial class CriterionItemWindow : Window
    {
        public CriterionItemWindow(ItemViewModel<Criterion> viewModel)
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
