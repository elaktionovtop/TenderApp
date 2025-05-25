using Microsoft.Extensions.DependencyInjection;

using System.Collections.ObjectModel;

using TenderApp.Models;
using TenderApp.Services;

namespace TenderApp.ViewModels
{
    public partial class TenderItemViewModel : ItemViewModel<Tender>
    {
        private UserService _service =
            App.Services.GetRequiredService<UserService>();

        public ObservableCollection<User> Users { get; }

        public TenderItemViewModel(TenderService service) 
            : base(service)
        {
            Users = _service
                .GetAll().ToObservableCollection();
        }

        public TenderItemViewModel(TenderService service, Tender item)
            : base(service, item)
        {
            Users = _service
                .GetAll().ToObservableCollection();
        }

        public override void OnApply()
        {
            Item.CreatedById = Item?.CreatedBy?.Id ?? 0;
        }
    }
}
