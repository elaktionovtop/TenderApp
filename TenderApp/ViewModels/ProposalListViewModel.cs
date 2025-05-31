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


        public  override void GetData()
        {
            if(BuyerId == 0)
            {
                Items = ((ProposalService)_service)
                .GetByTenderId(TenderId)
                .ToObservableCollection();
            }
            else
            {
                Items = ((ProposalService)_service)
                .GetByTenderIdBuyerId(TenderId, BuyerId)
                .ToObservableCollection();
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
