using System.ComponentModel.DataAnnotations.Schema;

namespace TenderApp.Models
{
    public class Proposal
    {
        public int Id { get; set; }

        public bool IsWinner { get; set; }
        public string? Comment { get; set; }

        public int TenderId { get; set; }
        public Tender Tender { get; set; } = null!;

        public int ByerId { get; set; }
        public User Byer { get; set; } = null!;

        public ICollection<CriterionValue> Values { get; set; }
            = [];

        [NotMapped]
        public Dictionary<int, string> ValueMap 
            => Values.ToDictionary(v => v.TenderCriterion.CriterionId,
                v => v.Value);
    }
}
