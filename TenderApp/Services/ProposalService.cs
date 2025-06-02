using Microsoft.EntityFrameworkCore;

using System.Diagnostics;
using System.IO;

using TenderApp.Data;
using TenderApp.Models;

using Xceed.Words.NET;

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

        public void CreateContracts(int tenderId)
        {
            var proposals = GetByTenderId(tenderId).Where(p => p.IsWinner);

            foreach(var p in proposals)
            {
                CreateContract(p);
            }
        }

        public void CreateContract(Proposal proposal)
        {
            Debug.WriteLine(proposal.Byer.Name);
            var fileName = $"Contract_{proposal.Id}.docx";
            var templatePath = @"..\..\..\Templates\ContractTemplate.docx";
            var outputPath = Path.Combine(@"..\..\..\Contracts", fileName);

            var doc = DocX.Load(templatePath);

            doc.ReplaceText("{Date}", DateTime.Now.ToShortDateString());
            doc.ReplaceText("{Buyer}", proposal?.Byer?.Name  ?? " *** ");
            doc.ReplaceText("{Product}", proposal?.Tender?.Product ?? " *** ");
            doc.ReplaceText("{TenderId}", proposal?.Tender?.Id.ToString());
            doc.ReplaceText("{Quantity}", proposal?.Tender.Quantity.ToString() );
            doc.ReplaceText("{TotalPrice}", proposal?.Tender.Budget.ToString() );
            doc.ReplaceText("{UnitPrice}", " *** ");
            doc.ReplaceText("{Manager}", proposal?.Tender.CreatedBy.Name ?? " *** " );

            doc.SaveAs(outputPath);
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
                throw new ArgumentException("Proposal tender is not specified");

            if(item.Byer is null)
                throw new ArgumentException("Proposal participant is not specified");

            foreach (var proposal in item.Values)
            {
                if(proposal.Value is null)
                    throw new ArgumentException("Proposal values are not specified");
            }
        }

        //protected override string GetDeleteErrorMessage
        //    (Proposal item)
        //    => "Unable to delete proposal: "
        //    + "there is data associated with it.";
    }
}
