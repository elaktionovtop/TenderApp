using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.DependencyInjection;

using System.Collections.ObjectModel;
using System.Diagnostics;
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

        protected override ItemViewModel<TenderCriterion> CreateItemViewModel
            (IDbService<TenderCriterion> service, TenderCriterion item)
            => new TenderCriterionItemViewModel((TenderCriterionService)service, item);

        protected override ItemViewModel<TenderCriterion> CreateItemViewModel
            (IDbService<TenderCriterion> service)
            => new TenderCriterionItemViewModel((TenderCriterionService)service,
                TenderId);

        protected override Window CreateItemDialog
            (ItemViewModel<TenderCriterion> itemViewModel)
            => new TenderCriterionItemWindow(itemViewModel);

    }
}
