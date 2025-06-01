using Microsoft.EntityFrameworkCore;

using System.Windows.Media.Media3D;

using TenderApp.Data;
using TenderApp.Models;

namespace TenderApp.Services
{
    public class TenderCriterionService(TenderDbContext db) 
        : DbService<TenderCriterion>(db)
    {
        public IEnumerable<TenderCriterion> GetByTenderId(int tenderId)
            => (db.TenderCriteria
            .Where(tc => tc.TenderId == tenderId)
                .Include(it => it.Tender)
                .Include(it => it.Criterion))
        ;

        public override TenderCriterion Clone(TenderCriterion source)
        {
            return new()
            {
                Id = source.Id,
                Weight = source.Weight,
                IsRequired = source.IsRequired,

                TenderId = source.TenderId,
                CriterionId = source.CriterionId,

                Tender = source.Tender,
                Criterion = source.Criterion
            };
        }

        public override void CopyTo(TenderCriterion source, 
            TenderCriterion target)
        {
            target.Id = source.Id;
            target.Weight = source.Weight;
            target.IsRequired = source.IsRequired;

            target.TenderId = source.TenderId;
            target.CriterionId = source.CriterionId;

            target.Tender = source.Tender;
            target.Criterion = source.Criterion;
        }

        public override void Validate(TenderCriterion item)
        {
            if(item.Weight < 1 || item.Weight > 100)
                throw new ArgumentException
                    ("Вес должен быть в пределах 1 - 100");
        }

        protected override string GetDeleteErrorMessage(TenderCriterion item)
            => "Невозможно удалить критерий тендера: "
            + "он содержит связанные заявки.";
    }
}
