namespace TenderApp.Models
{
    public class ProposalValue
    {
        public int Id { get; set; }

        public int ProposalId { get; set; }
        public Proposal Proposal { get; set; } = null!;

        public int CriterionId { get; set; }
        public Criterion Criterion { get; set; } = null!;

        public string Value { get; set; } = null!;
        public int? Score { get; set; } // выставляется CategoryManager’ом
    }
}
