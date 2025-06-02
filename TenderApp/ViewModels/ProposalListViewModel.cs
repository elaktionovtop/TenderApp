using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Diagnostics;
using System.Windows;

using TenderApp.Models;
using TenderApp.Services;
using TenderApp.Views;

namespace TenderApp.ViewModels
{
    public partial class ProposalListViewModel
        (IDbService<Proposal> service)
        : ListViewModel<Proposal>(service)
    {
        public int TenderId { get; set; }
        public int BuyerId { get; set; }

        [ObservableProperty]
        Visibility _buyerVisibility;

        //  вычисление победителя по критериям
        [RelayCommand]
        private void CalculateWinner()
        {
            Debug.WriteLine(nameof(CalculateWinner));
            var winner = ((ProposalService)_service).SelectWinner(TenderId);
            GetData(); // обновит Items и SelectedItem
        }

        //  создание контракта (контрактов)
        [RelayCommand]
        private void CreateContract()
        {
            Debug.WriteLine(nameof(CreateContract));

            ((ProposalService)_service)
                .CreateContracts(TenderId);
        }

        public override void GetData()
        {
            if(BuyerId == 0) // окно организатора
            {
                Items = ((ProposalService)_service)
                .GetByTenderId(TenderId)
                .ToObservableCollection();
                BuyerVisibility = Visibility.Visible;
            }
            else    // окно участника
            {
                Items = ((ProposalService)_service)
                .GetByTenderIdBuyerId(TenderId, BuyerId)
                .ToObservableCollection();
                BuyerVisibility = Visibility.Hidden;
            }

            SelectedItem = Items.FirstOrDefault();
        }

        protected override ItemViewModel<Proposal> CreateItemViewModel
            (IDbService<Proposal> service, Proposal item)
            => new ProposalItemViewModel((ProposalService)service, 
                item, BuyerId);

        protected override ItemViewModel<Proposal> CreateItemViewModel
            (IDbService<Proposal> service)
            => new ProposalItemViewModel((ProposalService)service, 
                TenderId, BuyerId);

        protected override Window CreateItemDialog
            (ItemViewModel<Proposal> itemViewModel)
            => new ProposalItemWindow(itemViewModel);
    }
}
