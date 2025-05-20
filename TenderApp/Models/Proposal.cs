using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenderApp.Models
{
    public class Proposal
    {
        public int Id { get; set; }

        public int TenderId { get; set; }
        public Tender Tender { get; set; } = null!;

        public int ByerId { get; set; }
        public User Byer { get; set; } = null!;

        public bool IsWinner { get; set; }
        public string? Comment { get; set; }

        public ICollection<ProposalValue> Values { get; set; } 
            = new List<ProposalValue>();
    }
}
