using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

using TenderApp.Services;
using TenderApp.Views;

namespace TenderApp.ViewModels
{
    public abstract partial class ListViewModel<T> 
        : ObservableObject 
        where T : class, new()
    {
        protected readonly IDbService<T> _service;

        [ObservableProperty]
        protected ObservableCollection<T> _items;

        [ObservableProperty]
        protected T? _selectedItem;

        protected ListViewModel(IDbService<T> service)
        {
            Debug.WriteLine(nameof(ListViewModel<T>));

            _service = service;
            //GetData();
            //Items = _service.GetAll().ToObservableCollection();
            //if(Items.Count > 0)
            //{
            //    SelectedItem = Items[0];
            //}
        }

        public virtual void GetData()
        {
            Items = _service.GetAll().ToObservableCollection();
            if(Items.Count > 0)
            {
                SelectedItem = Items[0];
            }
        }

        [RelayCommand]
        protected virtual void CreateItem()
        {
            Debug.WriteLine(nameof(CreateItem));

            var itemViewModel = CreateItemViewModel(_service);
            try
            {
                if(ShowItemDialog(itemViewModel) == true)
                {
                    _service.Add(itemViewModel.Item);
                    Items.Add(itemViewModel.Item);
                    SelectedItem = itemViewModel.Item;
                }
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, 
                    "Ошибка в данных при добавление объекта",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        [RelayCommand]
        protected virtual void EditItem()
        {
            Debug.WriteLine(nameof(EditItem));

            if(SelectedItem is null)
            {
                return;
            }

            var itemViewModel = CreateItemViewModel(_service, SelectedItem);
            try
            {
                if(ShowItemDialog(itemViewModel) == true)
                {
                    _service.Update(SelectedItem);
                    CollectionViewSource.GetDefaultView(Items).Refresh();
                    var currenItem = SelectedItem;
                    SelectedItem = null;
                    SelectedItem = currenItem;
                }
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, 
                    "Ошибка в данных при изменении объекта",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        [RelayCommand]
        protected virtual void DeleteItem()
        {
            Debug.WriteLine(nameof(DeleteItem));

            if(SelectedItem is null)
            {
                return;
            }

            try
            {
                int index = Items.IndexOf(SelectedItem);
                _service.Remove(SelectedItem);
                Items.Remove(SelectedItem);

                // установить новый выбор
                if(Items.Count > 0)
                {
                    if(index >= Items.Count)
                        index = Items.Count - 1;

                    SelectedItem = Items[index];
                }
                else
                {
                    SelectedItem = null;
                }
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка удаления",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        protected bool? ShowItemDialog(ItemViewModel<T> itemViewModel)
        {
            var dialog = CreateItemDialog(itemViewModel);
            // текущее окно, которое открывает диалог
            dialog.Owner = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.IsActive);
            return dialog.ShowDialog();
        }

        protected abstract ItemViewModel<T> CreateItemViewModel
            (IDbService<T> service, T item);

        protected abstract ItemViewModel<T> CreateItemViewModel
            (IDbService<T> service);

        protected abstract Window CreateItemDialog(ItemViewModel<T> itemViewModel);

    }
}
