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
            if(string.IsNullOrWhiteSpace(item.Product))
                throw new ArgumentException
                    ("Наименование продукта не указано");

            if(item.Budget <= 0)
                throw new ArgumentException("Бюджен не указан");

            if(item.Quantity <= 0)
                throw new ArgumentException("Количество не указано");

            if(item.CreatedBy is null)
                throw new ArgumentException("Менеджер не указан");

            if(item.Criteria is null)
                throw new ArgumentException("Критерии не указаны");
        }

        protected override string GetDeleteErrorMessage(Tender item)
            => "Невозможно удалить тендер: "
            + "он содержит связанные заявки.";
    }
}
