namespace TenderApp.Models
{
    public class TenderCriterion
    {
        public int Id { get; set; }

        public int TenderId { get; set; }
        public Tender Tender { get; set; } = null!;

        public int CriterionId { get; set; }
        public Criterion Criterion { get; set; } = null!;

        public double Weight { get; set; }
        public bool IsRequired { get; set; }
    }
}
