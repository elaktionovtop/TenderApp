namespace TenderApp.Models
{
    public class TenderCriterion
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public bool IsRequired { get; set; }

        public int TenderId { get; set; }
        public int CriterionId { get; set; }

        public Tender Tender { get; set; } = null!;
        public Criterion Criterion { get; set; } = null!;
    }
}
