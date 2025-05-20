using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Microsoft.Extensions.DependencyInjection;
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
            Loaded += (s, e) => FocusSelectedRow(dataGrid);

            if(viewModel.Items is System.Collections.Specialized
                .INotifyCollectionChanged notify)
            {
                notify.CollectionChanged += (s, e) =>
                {
                    Application.Current.Dispatcher.InvokeAsync
                        (() => FocusSelectedRow(dataGrid),
                            System.Windows.Threading
                                .DispatcherPriority.ContextIdle);
                };
            }
        }

        private void DataGrid_MouseDoubleClick(object sender,
            MouseButtonEventArgs e)
        {
            var vm = DataContext as TenderListViewModel;
            if(vm?.EditItemCommand.CanExecute(null) == true)
            {
                vm.EditItemCommand.Execute(null);
                e.Handled = true;
            }
        }

        private void DataGrid_PreviewKeyDown(object sender, 
            KeyEventArgs e)
        {
            var vm = DataContext as TenderListViewModel;

            if(e.Key == Key.Enter
                && vm?.EditItemCommand.CanExecute(null) == true)
            {
                vm.EditItemCommand.Execute(null);
                e.Handled = true;
            }

            if(e.Key == Key.Delete
                && vm?.DeleteItemCommand.CanExecute(null) == true)
            {
                vm.DeleteItemCommand.Execute(null);
                e.Handled = true;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if(grid.SelectedItem is not null)
            {
                grid.ScrollIntoView(grid.SelectedItem);
                //grid.Focus();
                Application.Current.Dispatcher.InvokeAsync
                    (() => FocusSelectedRow(grid),
                        System.Windows.Threading
                            .DispatcherPriority.ContextIdle);
            }
        }

        private void FocusSelectedRow(DataGrid grid)
        {
            if(grid.SelectedItem != null)
            {
                grid.ScrollIntoView(grid.SelectedItem);
                if(grid.ItemContainerGenerator.ContainerFromItem
                    (grid.SelectedItem) is DataGridRow row)
                {
                    row.MoveFocus(new TraversalRequest
                        (FocusNavigationDirection.Next));
                }
            }
        }
    }
}