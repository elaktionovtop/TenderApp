namespace TenderApp.Models
{
    public class Contract
    {
        public int Id { get; set; }

        public int ProposalId { get; set; }
        public Tender Proposal { get; set; } = null!;

        public string Details { get; set; } = null!;
        public string FilePath { get; set; } = null!;
    }
}
