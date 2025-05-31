using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using TenderApp.Models;
using TenderApp.Services;
using TenderApp.Views;

namespace TenderApp.ViewModels
{
    //internal class ProposalListViewModel
    public partial class ProposalListViewModel
        (IDbService<Proposal> service)
        : ListViewModel<Proposal>(service)
    {
        public int TenderId { get; set; }
//        public int ByerId { get; set; }

        public  override void GetData()
        {
            Items = ((ProposalService)_service)
                .GetByTenderId(TenderId)
                .ToObservableCollection();

            SelectedItem = Items.FirstOrDefault();
        }

        protected override ItemViewModel<Proposal> CreateItemViewModel
            (IDbService<Proposal> service, Proposal item)
            => new ProposalItemViewModel((ProposalService)service, item);

        protected override ItemViewModel<Proposal> CreateItemViewModel
            (IDbService<Proposal> service)
            => new ProposalItemViewModel((ProposalService)service, 
                TenderId);

        protected override Window CreateItemDialog
            (ItemViewModel<Proposal> itemViewModel)
            => new ProposalItemWindow(itemViewModel);
    }
}
