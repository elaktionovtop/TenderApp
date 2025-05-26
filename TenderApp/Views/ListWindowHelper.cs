using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using TenderApp.ViewModels;

namespace TenderApp.Views
{
    public static class ListWindowHelper
    {
        public static void PreviewKeyDown<T>(DataGrid grid,
            ListViewModel<T> viewModel, KeyEventArgs e)
            where T : class, new()
        {
            if(e.Key == Key.Enter
                && viewModel.EditItemCommand.CanExecute(null))
            {
                viewModel.EditItemCommand.Execute(null);
                e.Handled = true;
            }

            if(e.Key == Key.Delete
                && viewModel.DeleteItemCommand.CanExecute(null))
            {
                viewModel.DeleteItemCommand.Execute(null);
                e.Handled = true;
            }
        }

        public static void MouseDoubleClick<T>(DataGrid grid,
            ListViewModel<T> viewModel, MouseButtonEventArgs e)
            where T : class, new()
        {
            if(viewModel?.EditItemCommand.CanExecute(null) == true)
            {
                viewModel.EditItemCommand.Execute(null);
                e.Handled = true;
            }
        }

        public static void FocusSelectedRow(DataGrid dataGrid)
        {
            if(dataGrid.SelectedItem is not null)
            {
                Application.Current.Dispatcher.InvokeAsync
                    (() => MoveFocus(dataGrid), System.Windows.Threading
                       .DispatcherPriority.ContextIdle);
            }
        }

        public static void MoveFocus(DataGrid dataGrid)
        {
            if(dataGrid.SelectedItem != null)
            {
                dataGrid.ScrollIntoView(dataGrid.SelectedItem);
                if (dataGrid.ItemContainerGenerator
                    .ContainerFromItem(dataGrid.SelectedItem) 
                    is DataGridRow row)
                {
                    row.MoveFocus(new TraversalRequest
                        (FocusNavigationDirection.Next));
                }
            }
        }
    }
}
