using TenderApp.Models;
using TenderApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace TenderApp.ViewModels
{
    public partial class UserItemViewModel : ItemViewModel<User>
    {
        private readonly RoleService _roleService =
            App.Services.GetRequiredService<RoleService>();

        public ObservableCollection<Role> Roles { get; }

        public UserItemViewModel(UserService service) 
            : base(service)
        {
            Roles = _roleService
                .GetAll().ToObservableCollection();
        }

        public UserItemViewModel(UserService service, User item)
            : base(service, item)
        {
            Roles = _roleService
                .GetAll().ToObservableCollection();
        }

        public override void OnApply()
        {
            Item.RoleId = Item?.Role?.Id ?? 0;
        }
    }
}
