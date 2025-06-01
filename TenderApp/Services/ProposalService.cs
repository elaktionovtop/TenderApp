using Microsoft.EntityFrameworkCore;

using TenderApp.Data;
using TenderApp.Models;

namespace TenderApp.Services
{
    public class ProposalService(TenderDbContext db)
        : DbService<Proposal>(db)
    {
        public IEnumerable<Proposal>GetByTenderId(int tenderId)
            => (_db.Proposals
            .Where(p => p.TenderId == tenderId)
            .Include(p => p.Tender)
            .Include(p => p.Byer)
            .Include(p => p.Values)
                .ThenInclude(v => v.TenderCriterion)
                    .ThenInclude(tc => tc.Criterion))
            ;

        public IEnumerable<Proposal> GetByTenderIdBuyerId(int tenderId, int byerId)
            => GetByTenderId(tenderId).Where(p => p.ByerId == byerId);

        public Proposal? CalculateBest(int tenderId)
            => GetByTenderId(tenderId)
                .MaxBy(p => p.Values.Sum(v =>
                    (v.Score ?? 0) * (v.TenderCriterion?.Weight ?? 0)));

        public Proposal? SelectWinner(int tenderId)
        {
            var proposals = GetByTenderId(tenderId);
            var best = proposals.MaxBy(p => p.Values.Sum(v =>
                    (v.Score ?? 0) * (v.TenderCriterion?.Weight ?? 0)));

            foreach(var p in proposals)
                p.IsWinner = (p == best);

            _db.SaveChanges();

            return best;
        }

        public override Proposal Clone(Proposal source)
        {
            return new()
            {
                Id = source.Id,
                IsWinner = source.IsWinner,
                Comment = source.Comment,

                TenderId = source.TenderId,
                Tender = source.Tender,

                ByerId = source.ByerId,
                Byer = source.Byer,

                Values = source.Values
            };
        }

        public override void CopyTo(Proposal source, Proposal target)
        {
            target.Id = source.Id;
            target.IsWinner = source.IsWinner;
            target.Comment = source.Comment;

            target.TenderId = source.TenderId;
            target.Tender = source.Tender;

            target.ByerId = source.ByerId;
            target.Byer = source.Byer;

            target.Values = source.Values;
        }
        public override void Validate(Proposal item)
        {
            if(item.Tender is null)
                throw new ArgumentException("Тендер заявки не указан");

            if(item.Byer is null)
                throw new ArgumentException("Участник заявки не указан");

            foreach (var proposal in item.Values)
            {
                if(proposal.Value is null)
                    throw new ArgumentException("Значения заявки не указаны");
            }
        }

        //protected override string GetDeleteErrorMessage
        //    (Proposal item)
        //    => "Невозможно удалить заявку: "
        //    + "есть данные связанные с ней.";
    }
}
