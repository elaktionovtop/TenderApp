using System.Windows;

using TenderApp.Models;
using TenderApp.Services;
using TenderApp.Views;

namespace TenderApp.ViewModels
{
    public partial class UserListViewModel(IDbService<User> service)
            : ListViewModel<User>(service)
    {
        protected override ItemViewModel<User> CreateItemViewModel
            (IDbService<User> service, User item)
            => new UserItemViewModel((UserService)service, item);

        protected override ItemViewModel<User> CreateItemViewModel
            (IDbService<User> service)
            => new UserItemViewModel((UserService)service);

        protected override Window CreateItemDialog
            (ItemViewModel<User> itemViewModel)
            => new UserItemWindow(itemViewModel);
    }
}

