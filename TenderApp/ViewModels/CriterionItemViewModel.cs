using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.DependencyInjection;

using System.Collections.ObjectModel;
using System.Windows.Controls;

using TenderApp.Models;
using TenderApp.Services;

namespace TenderApp.ViewModels
{
    public partial class CriterionItemViewModel : ItemViewModel<Criterion>
    {

        public ObservableCollection<EnumDisplay<CriterionType>> CriteriaTypes
        { get; } = Criterion.CriteriaTypes;

        [ObservableProperty]
        private EnumDisplay<CriterionType> _selectedCriterionType;


        public CriterionItemViewModel(CriterionService service)
            : base(service)
        {
            SelectedCriterionType = CriteriaTypes[0];
        }

        public CriterionItemViewModel(CriterionService service, 
            Criterion item)
            : base(service, item)
        {
            SelectedCriterionType = CriteriaTypes[0];
        }
    }
}

