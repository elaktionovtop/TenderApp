using System.Windows;

using TenderApp.Models;
using TenderApp.Services;
using TenderApp.Views;

namespace TenderApp.ViewModels
{
    public partial class CriterionListViewModel(IDbService<Criterion> service)
        : ListViewModel<Criterion>(service)
    {
        protected override ItemViewModel<Criterion> CreateItemViewModel(IDbService<Criterion> service, Criterion item)
            => new CriterionItemViewModel((CriterionService)service, item);

        protected override ItemViewModel<Criterion> CreateItemViewModel(IDbService<Criterion> service)
            => new CriterionItemViewModel((CriterionService)service);

        protected override Window CreateItemDialog(ItemViewModel<Criterion> itemViewModel)
            => new CriterionItemWindow(itemViewModel);
    }
}
