using TenderApp.Models;
using TenderApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace TenderApp.ViewModels
{
    public partial class UserItemViewModel : ItemViewModel<User>
    {
        private RoleService _service =
            App.Services.GetRequiredService<RoleService>();

        public ObservableCollection<Role> Roles { get; }

        public UserItemViewModel(UserService service) 
            : base(service)
        {
            Roles = _service
                .GetAll().ToObservableCollection();
        }

        public UserItemViewModel(UserService service, User item)
            : base(service, item)
        {
            Roles = _service
                .GetAll().ToObservableCollection();
        }

        public override void OnApply()
        {
            Item.RoleId = Item?.Role?.Id ?? 0;
        }
    }
}
