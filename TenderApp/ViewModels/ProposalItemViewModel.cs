using Microsoft.Extensions.DependencyInjection;

using System.Collections.ObjectModel;

using TenderApp.Models;
using TenderApp.Services;

namespace TenderApp.ViewModels
{
    public partial class ProposalItemViewModel 
        : ItemViewModel<Proposal>
    {
        private readonly UserService _userService =
            App.Services.GetRequiredService<UserService>();

        public ObservableCollection<User> Users { get; }

        public ProposalItemViewModel(ProposalService service, 
            int tenderId)
            : base(service)
        {
            Users = _userService
                .GetAll().ToObservableCollection();

            Item.TenderId = tenderId;
            Item.Tender = App.Services
                .GetRequiredService<TenderService>()
                .FindById(tenderId);

            // Получить критерии тендера из сервиса
            var criteria = App.Services
                .GetRequiredService<TenderCriterionService>()
                .GetByTenderId(tenderId);

            foreach(var criterion in criteria)
            {
                Item.Values.Add(new CriterionValue
                {
                    TenderCriterionId = criterion.Id,
                    TenderCriterion = criterion
                });
            }

        }

        public ProposalItemViewModel
            (ProposalService service, Proposal item)
            : base(service, item)
        {
            Users = _userService
                .GetAll().ToObservableCollection();
        }

        public override void OnApply()
        {
            Item.ByerId = Item?.Byer?.Id ?? 0;
        }
    }
}
