using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
