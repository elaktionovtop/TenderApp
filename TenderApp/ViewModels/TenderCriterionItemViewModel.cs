using Microsoft.Extensions.DependencyInjection;

using System.Collections.ObjectModel;

using TenderApp.Models;
using TenderApp.Services;

namespace TenderApp.ViewModels
{
    public partial class TenderCriterionItemViewModel 
        : ItemViewModel<TenderCriterion>
    {
        private readonly CriterionService _criterionService =
            App.Services.GetRequiredService<CriterionService>();

        public ObservableCollection<Criterion> Criteria { get; }

        public TenderCriterionItemViewModel(TenderCriterionService service,
            int TenderId) : base(service)
        {
            Criteria = _criterionService
                .GetAll().ToObservableCollection();
            Item.TenderId = TenderId;
        }

        public TenderCriterionItemViewModel(TenderCriterionService service,
            TenderCriterion item)
            : base(service, item)
        {
            Criteria = _criterionService
                .GetAll().ToObservableCollection();
        }

        public override void OnApply()
        {
            Item.CriterionId = Item?.Criterion?.Id ?? 0;
        }
    }
}
