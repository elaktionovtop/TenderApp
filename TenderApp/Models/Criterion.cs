using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

using TenderApp.Models;
using TenderApp.Services;

namespace TenderApp.Models
{
    public class Criterion
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public CriterionType Type { get; set; } 
            = CriterionType.Numeric;

        [NotMapped]
        public static ObservableCollection<EnumDisplay<CriterionType>>
            CriteriaTypes { get; } = new()
            {
                new(CriterionType.Numeric, "Числовой"),
                new(CriterionType.Text, "Текстовый")
            };

        [NotMapped]
        EnumDisplay<CriterionType> SelectedCriterionType;

        [NotMapped]
        public string CriterionTypeText => CriteriaTypes
            .FirstOrDefault(s => s.Value == Type).Display;
    }

    public enum CriterionType
    {
        Numeric,
        Text
    }
}
