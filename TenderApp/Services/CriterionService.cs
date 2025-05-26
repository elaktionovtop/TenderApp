using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TenderApp.Data;
using TenderApp.Models;

namespace TenderApp.Services
{
    public class CriterionService(TenderDbContext db) 
        : DbService<Criterion>(db)
    {
        public override Criterion Clone(Criterion source)
        {
            return new()
            {
                Id = source.Id,
                Name = source.Name,
                Type = source.Type
            };
        }

        public override void CopyTo(Criterion source, Criterion target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Type = source.Type;
        }

        public override void Validate(Criterion item)
        {
            if(string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException
                    ("Имя пользователя не указано");
        }

        protected override string GetDeleteErrorMessage(Criterion item)
            => "Невозможно удалить критерий: "
            + "есть данные связанные с ним.";
    }
}
