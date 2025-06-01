namespace TenderApp.Models
{
    public class CriterionValue
    {
        public int Id { get; set; }

        public int ProposalId { get; set; }
        public Proposal Proposal { get; set; } = null!;

        public int TenderCriterionId { get; set; }
        public TenderCriterion? TenderCriterion { get; set; }

        public string Value { get; set; } = null!;
        public int? Score { get; set; } // выставляется CategoryManager’ом
    }
}
