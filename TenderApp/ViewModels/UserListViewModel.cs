using TenderApp.Models;
using TenderApp.Services;

namespace TenderApp.ViewModels
{
    public partial class UserListViewModel(IDbService<User> service)
            : ListViewModel<User>(service)
    {
        protected override ItemViewModel<User> CreateItemViewModel(IDbService<User> service, User item)
        {
            return new UserItemViewModel((UserService)service, item);
        }

        protected override ItemViewModel<User> CreateItemViewModel(IDbService<User> service)
        {
            return new UserItemViewModel((UserService)service);
        }

        protected override bool? ShowItemDialog(ItemViewModel<User> itemViewModel)
            => new UserItemWindow((UserItemViewModel)itemViewModel).ShowDialog();
    }
}

