using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.DependencyInjection;

using System;
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

        [ObservableProperty]
        private bool _buyerMustBeSelected;

        public ProposalItemViewModel(ProposalService service, 
            int tenderId, int buyerId)
            : base(service)
        {
            Users = _userService
                .GetAll().ToObservableCollection();

            Item.TenderId = tenderId;
            Item.Tender = App.Services
                .GetRequiredService<TenderService>()
                .FindById(tenderId);
            Item.ByerId = buyerId;
            Item.Byer = App.Services
                .GetRequiredService<UserService>()
                .FindById(buyerId);

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

            BuyerMustBeSelected = buyerId == 0;
        }

        public ProposalItemViewModel
            (ProposalService service, Proposal item, int buyerId)
            : base(service, item)
        {
            Users = _userService
                .GetAll().ToObservableCollection();
            BuyerMustBeSelected = buyerId == 0;
        }

        public override void OnApply()
        {
            Item.ByerId = Item?.Byer?.Id ?? 0;
        }
    }
}
