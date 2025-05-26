using Microsoft.Extensions.DependencyInjection;

using System.Collections.ObjectModel;

using TenderApp.Models;
using TenderApp.Services;

namespace TenderApp.ViewModels
{
    public partial class TenderItemViewModel : ItemViewModel<Tender>
    {
        private readonly UserService _userService =
            App.Services.GetRequiredService<UserService>();

        public ObservableCollection<User> Users { get; }

        public TenderItemViewModel(TenderService service) 
            : base(service)
        {
            Users = _userService
                .GetAll().ToObservableCollection();
        }

        public TenderItemViewModel(TenderService service, Tender item)
            : base(service, item)
        {
            Users = _userService
                .GetAll().ToObservableCollection();
        }

        public override void OnApply()
        {
            Item.CreatedById = Item?.CreatedBy?.Id ?? 0;
        }
    }
}
