using TenderApp.Data;
using TenderApp.Models;

using Microsoft.EntityFrameworkCore;

namespace TenderApp.Services
{
    public class TenderService(TenderDbContext db) : DbService<Tender>(db)
    {
        public override IEnumerable<Tender> GetAll()
            => _db.Tenders
                .Include(it => it.CreatedBy)
                .Include(it => it.Criteria)
        ;

        public override Tender Clone(Tender source)
        {
            return new()
            {
                Id = source.Id,
                Product = source.Product,
                Budget = source.Budget,
                Quantity = source.Quantity,

                CreatedById = source.CreatedById,
                CreatedBy = source.CreatedBy,

                Criteria = source.Criteria,
                Proposals = source.Proposals
            };
        }

        public override void CopyTo(Tender source, Tender target)
        {
            target.Id = source.Id;
            target.Product = source.Product;
            target.Budget = source.Budget;
            target.Quantity = source.Quantity;

            target.CreatedById = source.CreatedById;
            target.CreatedBy = source.CreatedBy;

            target.Criteria = source.Criteria;
            target.Proposals = source.Proposals;
        }

        public override void Validate(Tender item)
        {
            if(string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException
                    ("Tender name is not specified");

            if(string.IsNullOrWhiteSpace(item.Product))
                throw new ArgumentException
                    ("Product name is not specified");

            if(item.Budget <= 0)
                throw new ArgumentException("Budget is not specified");

            if(item.Quantity <= 0)
                throw new ArgumentException("Quantity is not specified");

            if(item.CreatedBy is null)
                throw new ArgumentException("Category manager is not specified");

            if(item.Criteria is null)
                throw new ArgumentException("Criteria are not specified");
        }

        protected override string GetDeleteErrorMessage(Tender item)
            => "Unable to delete tender: "
            + "it contains related proposals.";
    }
}
