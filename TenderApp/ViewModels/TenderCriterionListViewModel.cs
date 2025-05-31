using System.Windows;

using TenderApp.Models;
using TenderApp.Services;
using TenderApp.Views;

namespace TenderApp.ViewModels
{
    public partial class TenderCriterionListViewModel
        (IDbService<TenderCriterion> service)
        : ListViewModel<TenderCriterion>(service)
    {
        public int TenderId { get; set; }

        public override void GetData()
        {
            Items = ((TenderCriterionService)_service)
                .GetByTenderId(TenderId)
                .ToObservableCollection();

            SelectedItem = Items.FirstOrDefault();
        }

        protected override ItemViewModel<TenderCriterion> CreateItemViewModel
            (IDbService<TenderCriterion> service, TenderCriterion item)
            => new TenderCriterionItemViewModel((TenderCriterionService)service, item);

        protected override ItemViewModel<TenderCriterion> CreateItemViewModel
            (IDbService<TenderCriterion> service)
            => new TenderCriterionItemViewModel(
                (TenderCriterionService)service,
                TenderId);

        protected override Window CreateItemDialog
            (ItemViewModel<TenderCriterion> itemViewModel)
            => new TenderCriterionItemWindow(itemViewModel);

    }
}
