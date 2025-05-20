using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenderApp.Models
{
    public class Tender
    {
        public int Id { get; set; }
        public string Product { get; set; } = null!;
        public decimal Budget { get; set; }
        public int Quantity { get; set; }

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; } = null!;

        public ICollection<TenderCriterion> Criteria { get; set; } 
            = [];
        public ICollection<Proposal> Proposals { get; set; } 
            = [];
    }
}
