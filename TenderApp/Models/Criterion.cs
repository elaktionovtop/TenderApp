namespace TenderApp.Models
{
    public class Criterion
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public CriterionType Type { get; set; }
    }

    public enum CriterionType
    {
        Numeric,
        Text
    }
}
