using Microsoft.Extensions.DependencyInjection;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using TenderApp.Models;
using TenderApp.ViewModels;

namespace TenderApp.Views
{
    public partial class CriterionListWindow : Window
    {
        public CriterionListWindow()
        {
            InitializeComponent();
            var viewModel = App.Services
                .GetRequiredService<CriterionListViewModel>();
            DataContext = viewModel;

            Loaded += (s, e)
                => ListWindowHelper.MoveFocus(dataGrid);

            viewModel.Items.CollectionChanged += (s, e)
                => ListWindowHelper.FocusSelectedRow(dataGrid);

            dataGrid.SelectionChanged += (s, e)
                => ListWindowHelper.FocusSelectedRow(dataGrid);

            dataGrid.MouseDoubleClick += (s, e) => ListWindowHelper
                .MouseDoubleClick<Criterion>(dataGrid, viewModel, e);

            dataGrid.PreviewKeyDown += (s, e) => ListWindowHelper
                .PreviewKeyDown<Criterion>(dataGrid, viewModel, e);
        }
    }
}
