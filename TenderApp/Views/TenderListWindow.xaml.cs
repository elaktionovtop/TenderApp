using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Microsoft.Extensions.DependencyInjection;

using TenderApp.Models;
using TenderApp.ViewModels;

namespace TenderApp.Views
{
    public partial class TenderListWindow : Window
    {
        public TenderListWindow()
        {
            InitializeComponent();
            var viewModel = App.Services
                .GetRequiredService<TenderListViewModel>();
            DataContext = viewModel;

            Loaded += (s, e)
                => ListWindowHelper.MoveFocus(dataGrid);

            viewModel.Items.CollectionChanged += (s, e)
                => ListWindowHelper.FocusSelectedRow(dataGrid);

            dataGrid.SelectionChanged += (s, e)
                => ListWindowHelper.FocusSelectedRow(dataGrid);

            dataGrid.MouseDoubleClick += (s, e) => ListWindowHelper
                .MouseDoubleClick<Tender>(dataGrid, viewModel, e);

            dataGrid.PreviewKeyDown += (s, e) => ListWindowHelper
                .PreviewKeyDown<Tender>(dataGrid, viewModel, e);
        }
    }
}

